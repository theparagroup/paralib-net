using System;
using System.Linq;
using System.Collections.Generic;
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

        Currently only supports identity columns. We'll need to add the following support to the 
        generator in future versions:

            [DatabaseGenerated(DatabaseGeneratedOption.None)]

        Future versions may use the [paralib_column_metadata] table to generate the [Display] and 
        other attributes. 

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

                //etc
            }

        While we still need to generate "friendly" class and member names, we could use the [Column]
        attribute here to explicitly declare all mappings and not rely on conventions.

    */

    public class MetadataGenerator:ClassGenerator
    {

        public MetadataGenerator(IClassWriter writer, IConvention convention, Dictionary<string, Table> tables, ClassOptions classOptions) : base(writer, convention, tables, classOptions)
        {
        }


        protected override void OnGenerate(Table table, string className)
        {
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

            int i = 0;

            foreach (Column column in table.Columns.Values)
            {
                WriteLine();

                //add metadata
                string displayName = Convention.GetDisplayName(column.Name);

                //key & order info (useful for EF on unconventionally-named or multi-valued key columns)
                if (column.IsPrimary) WriteLine($"\t\t[Key, Column(Order = {i})]");

                //foreign key info (useful for EF when the principal end of a one-to-one cannot be determined)
                if (column.IsForeign)
                {
                    foreach (Relationship fk in table.ForeignKeys)
                    {
                        if (fk.OnColumn==column.Name)
                        {
                            //is other table included in our table list (not 'skipped')?
                            if ((from t in _tables.Values where t.Name == fk.OtherTable select t).Count() > 0)
                            {
                                WriteLine($"\t\t[ForeignKey(\"{Convention.GetEntityName(fk.OnColumn)}\")]");
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

                ++i;
            }

            WriteLine("\t}");

            if (ClassOptions.Namespace != null) WriteLine("}");

        }


    }
}
