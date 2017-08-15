using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using com.paralib.Dal.Metadata;

namespace com.paralib.Migrations.CodeGen
{
    /*
        Creates a metadata object that is attached to the model via an attribute.
        
        Primarily for validation, but the [Key] and [Column] attributes are used by EF as well.
        
        Note, EF generally uses conventions for keys (e.g., "Id", "Student.StudentID") but when
        your mappings do not follow convention or you have compound keys, you must excplictly
        specify them and their order. The ordinal doesn't matter but the order does.

        So we have a rule to always generate the explicit attributes. We could (and maybe should)
        place these in the POCOs, but we want those files to be "clean" (i.e., without any attributes).

        You can only have one metadata class associated with a given class.

        Column ordering for PKs and FKs is a bit of a pain. The [Column(Order=1)] attribute indicates 
        the order of the column *within the table* (no duplicate ordinals), but where it is used in 
        multi-valued foreign key situations, it is the relative order of the multiple columns on the entity
        class that matters. If the relative orders do not match, then the join will be wrong.

        To address this issue, there are two solutions:

            1] Add [ForeignKey("key1,key2")] attributes to the reference navigation property

            2] Use [ForeignKey("NavProperty"), Column(Order=n)] on the foreign key properties, and spread 
            out the column ordinals so that potential multi-valued primary and foreign keys preserve
            their relative order and don't overlap.

        Currently we are going with #2, reserving 1000 columns for primary keys and 1000 for each foreign 
        key. We use the actual ordering of the columns in the constraint (PK, FK) for the relative ordering
        of the columns within a key.

        Unfortunately, it seems that either way, it is possible to have the order of the FK columns not match 
        the order of the PKs on the entity class. For example, a compound PK key referenced by multiple tables,
        each using a different ordering of columns in the constraint:

            table1      PRIMARY KEY (foo, bar)
            table2      FOREIGN KEY (foo, bar) REFERENCES table1(foo, bar)
            table3      FOREIGN KEY (bar, foo) REFERENCES table1(bar, foo)

        This means the ordering of the columns in the FK must match the order in the PK!

        This also means that multi-valued FKs where one column is also a primary key and another column refers 
        to a unique index are not supported!

        You can always skip the table and code it up manually.

        BTW, we only order PK and FK columns on the entity classes.

        TODO: Currently only supports identity columns. We'll need to add the following support to the 
        generator in future versions:

            [DatabaseGenerated(DatabaseGeneratedOption.None)]

        TODO: Future versions may use the [paralib_column_metadata] table to generate the [Display] and 
        other attributes. 

        Example of generated file:

            [MetadataType(typeof(ParentMetadata))]
	        public partial class Parent
	        {
	        }

            public class ParentMetadata
    	    {
		        [Key, Column(Order = 0)]
		        [Display(Name="Id")]
		        [Required(ErrorMessage="Id is required")]
		        [ParaType(ParaTypes.Key)]
		        public object Id;

		        [ForeignKey("Author"), Column(Order = 1000)]
		        [Display(Name="Foo")]
		        [Required(ErrorMessage="Foo is required")]
		        public object Foo;

		        [ForeignKey("Author"), Column(Order = 1001)]
		        [Display(Name="Bar")]
		        [Required(ErrorMessage="Bar is required")]
		        public object Bar;

                //etc
            }

        TODO: While we still need to generate "friendly" class and member names, we could use the [Column]
        attribute here to explicitly declare all mappings and not rely on conventions.

    */

    public class MetadataGenerator:ClassGenerator
    {

        public MetadataGenerator(IClassWriter writer, IConvention convention, Dictionary<string, Table> tables, ClassOptions classOptions) : base(writer, convention, tables, classOptions)
        {
        }
        

        internal static string GetReferenceName(IConvention convention, Relationship foreignKey, bool inverse=false)
        {
            /*

                For References based on this FK,
                    we use the FK property on this table (OnColumn) in single-valued cases
                    we use the referring table (OtherTable) in multi-valued

                created_by_user_id => public int CreatedByUserId { get; set; }
                created_by_user_id => public virtual EfUser CreatedByUser { get; set; }
                
                user_first_name => public string UserFirstName { get; set; }
                user_last_name => public string UserLastName { get; set; }
                user_first_name, user_last_name => public virtual EfUser User { get; set; }

                For the inverse (e.g., [InverseProperty]), we reverse it.

            */

            if (foreignKey.Columns.Count == 1)
            {
                //for single-valued FKs, we can use the property name (as derived from the column name) minus "id"
                string columnName = inverse ? foreignKey.Columns[0].OtherColumn : foreignKey.Columns[0].OnColumn;
                columnName = Regex.Replace(columnName.ToLower(), "_id$", m => "");
                columnName = convention.GetPropertyName(columnName);
                return columnName;
            }
            else
            {
                //for multi-valued FKs, we should use the table name
                return convention.GetClassName(inverse ? foreignKey.OnTable : foreignKey.OtherTable, Pluralities.Singular);
            }

        }

        protected override void OnGenerate(Table table, string className)
        {
            //for debugging
            if (className == "Book")
            {
                int x = 1;
            }

            WriteLine("using System;");
            WriteLine("using System.ComponentModel.DataAnnotations;");
            WriteLine("using System.ComponentModel.DataAnnotations.Schema;");
            WriteLine("using com.paralib.DataAnnotations;");
            if (ClassOptions.Namespace != null) WriteLine($"using {ClassOptions.Namespace}{(ClassOptions.SubNamespace != null ? "." + ClassOptions.SubNamespace : "")}{(ClassOptions.Parameter != null ? "." + ClassOptions.Parameter : "")};\n");
            WriteLine();

            //namespace Foo.Models {
            if (ClassOptions.Namespace!=null) WriteLine($"namespace {ClassOptions.Namespace}{(ClassOptions.SubNamespace!=null?"."+ ClassOptions.SubNamespace:"")}\n{{");

            WriteLine($"\t[MetadataType(typeof({className}Metadata))]");
            WriteLine($"\tpublic partial class {className}");
            WriteLine("\t{");
            WriteLine("\t}");
            if (ClassOptions.Namespace != null) WriteLine("}");
            WriteLine();

            //namespace Foo.Models.Metadata {
            if (ClassOptions.Namespace != null) WriteLine($"namespace {ClassOptions.Namespace}{(ClassOptions.SubNamespace != null ? "." + ClassOptions.SubNamespace : "")}{(ClassOptions.Parameter != null ? "." + ClassOptions.Parameter : "")}\n{{");

            WriteLine($"\tpublic class {className}Metadata");
            WriteLine("\t{");

            foreach (Column column in table.Columns.Values)
            {
                WriteLine();

                //add metadata
                string displayName = Convention.GetDisplayName(column.Name);

                //key & order info (useful for EF on unconventionally-named or multi-valued key columns)
                if (column.IsPrimary)
                {
                    //TODO only add the order on multivalued PK

                    //note: we use the ordinal of the column in the PK constraint (as given by the db provider).
                    int order = table.PrimaryKeys.IndexOf(column);
                    WriteLine($"\t\t[Key, Column(Order = {order})]");
                }

                //foreign key info (useful for EF when the principal end of a one-to-one cannot be determined)
                if (column.IsForeign)
                {
                    foreach (Relationship fk in table.ForeignKeys.Values)
                    {
                        //add [ForeignKey] attribute using Reference name (via property or table name)

                        ColumnPair fkColumnPair = fk.Columns.Where(c => c.OnColumn == column.Name).FirstOrDefault();
                        if (fkColumnPair!=null)
                        {
                            //is other table included in our table list (not 'skipped')?
                            if ((from t in _tables.Values where t.Name == fk.OtherTable select t).Count() > 0)
                            {
                                /*

                                    Note: we create the foreign key reference property that our attributes refer 
                                    to in the EfGenerator.

                                */


                                //see if we need to append order attribute (for multi-valued foreign keys)
                                //  ", Column(Order = n)]"
                                string columnAttribute = "";    

                                //yes we have a multi-valued FK
                                if (fk.Columns.Count > 1)
                                {
                                    //TODO verify the FK order matches the order of the PK on the other table

                                    if (!column.IsPrimary)
                                    {
                                        //yes add it, we use the ordering of the FK constraint 
                                        var fkNames = table.ForeignKeys.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Key).ToList();
                                        int fkOrdinal = fkNames.IndexOf(fk.Name) + 1;
                                        int fkColumnOrder = fk.Columns.IndexOf(fkColumnPair) + (fkOrdinal * 1000);
                                        columnAttribute=$", Column(Order = {fkColumnOrder})";
                                    }
                                    else
                                    {
                                        //TODO verify the FK order matches the order of the PK on this table too!
                                    }
                                }

                                //created_by_user_id => [ForeignKey("CreatedByUser")]
                                //user_first_name, user_last_name => [ForeignKey("User")]
                                WriteLine($"\t\t[ForeignKey(\"{GetReferenceName(Convention, fk)}\"){columnAttribute}]");
                            }
                        }
                    }

                }

                //display
                WriteLine($"\t\t[Display(Name=\"{displayName}\")]");

                //required?
                if (!column.IsNullable) WriteLine($"\t\t[Required(ErrorMessage=\"{displayName} is required\")]");

                //paratype?
                if (column.Properties?.ParaType != null)
                {
                    WriteLine($"\t\t[ParaType(ParaTypes.{column.Properties?.ParaType})]");
                }
                else
                {
                    //stringlength?
                    if (column.ClrType == typeof(string) && column.Length.HasValue) WriteLine($"\t\t[StringLength({column.Length})]");
                }


                WriteLine($"\t\tpublic object {Convention.GetPropertyName(column.Name)};");

            }

            WriteLine("\t}");

            if (ClassOptions.Namespace != null) WriteLine("}");

        }


    }
}
