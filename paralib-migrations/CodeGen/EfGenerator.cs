using System;
using System.Linq;
using System.Collections.Generic;
using com.paralib.Dal.Metadata;
using com.paralib.Dal.Utils;
using System.Text.RegularExpressions;

namespace com.paralib.Migrations.CodeGen
{
    /*

***********************************************************************************************************************
***********************************************************************************************************************
***********************************************************************************************************************

        
        This generator creates the DbContext class, and sub-classes for each of the model 
        classes. These classes contain the EF navigation properties.

        We use attributes only, no fluent API.

        The model generator will have created a POCO that uses the paralib convention to map columns
        to properties. Note, however, the navigation properties only care about property names,
        not the column names.       

        The metadata generator will have created a metadata object that identifies the (multiple) 
        primary keys of the class and their order. Compound primary keys are therefore supported.

        However, we currently aren't supporting composite foreign keys in the navigation properties.

        Basic example:


            parents                 children
            --------   <one:many>   --------
            id (pk)                 id (pk)
                                    parent_id (fk)


        The parent is the "principal entity", and the column [parent].[id] is the "principal key".

        The child is the "dependent entity", and the column [child].[parent_id] is the "dependent key",
        which is also the foreign key.

        The simple POCO along with DbContext will support simple queries without relationship support.
        To add this we need to provide "navigation properties".

        Navigation properties may be "References" (scalars) or "Collections".

        The corresponding navigation property in the related entity is called the "Inverse Property".

        Terminology note: a "compound" key is a multi-column key where each column is a key as well,
        such as in a many-to-many table. A "composite" key is a multi-column key where at lease one
        column is not a key itself. We like to use "composite" for indexes, i.e., "single vs composite".

        Terminology note: a "many-to-many" relationship is a conceptual idea found in data modeling or in 
        ORMs such as EF. In the database, these relationships are implemented with "association tables",
        also known as junction, cross-reference, or intermediate tables.


        Navigation properties must be marked virtual to support lazy loading. Lazy loading can also be
        turned off for the whole DbContext. If it's not on, you must explicitly load the entities:


            var foo=from f in db.Foo.Include("Bars") select f;      //eager loading
            db.Entry(child).Reference(c => c.Parent).Load();        //reference
            db.Entry(parent).Collection(c => c.Children).Load();    //collection


***********************************************************************************************************************
***********************************************************************************************************************
***********************************************************************************************************************


        We'll start with the dependent entity first, which has a foreign key (note we sometimes combine the POCO, 
        metadata, and Ef class files in these examples):


            public class Child
            {
                public int Id { get; set; }
                public int ParentId { get; set; }
                public string Name { get; set; }

                //add reference navigation property
                public Parent Parent { get; set; }

            }

        Note: in these "one-to-many" examples, "zero-or-one-to-many" can be implemented by making
        the foriegn key on the dependent entity nullable.

        If your names follow your conventions, such as:

            fk column   ->  fk property ->  nav property
            parent_id   ->  ParentId    ->  Parent

        This works just fine. However, if EF can't figure things out from the conventions,
        you'll need to explicitly relate the navigation property to the foriegn key property.

        EF tries hard to figure it out, but you can confuse it. For example if the FK property 
        name is not related to either the principal entities's class name or the navigation 
        property's name, such as:

            public partial class Paper
            {
                public int Id { get; set; }

                public int CreatedByAuthorId { get; set; }

                public string Name { get; set; }

                public virtual Author CreatedBy { get; set; }

            }

        There is no way for EF to get from "Author" or "CreatedBy" to "CreatedByAuthorId".

        You can fix this in two ways:

                [ForeignKey("CreatedBy")]
                public int CreatedByAuthorId { get; set; }

        Or

                [ForeignKey("CreatedByAuthorId")]
                public Parent CreatedBy { get; set; }

***********************************************************************************************************************
***********************************************************************************************************************
***********************************************************************************************************************

        Let's add the inverse property (a collection) to the parent:


            public partial class Parent
            {
                public int Id { get; set; }
                public string Name { get; set; }

                //add collection navigation property
                public List<Child> Children { get; set; }
            }

        Again, if naming conventions hold, this just works.

        Note, you do not need to create both ends of the relationship to work with navigation
        properties. If you omit a FK for example, EF will create a "shaddow property" in the change
        tracker.

        Note as well, EF uses the class name here, and doesn't care about the property name. For example,
        this works just fine:


                public List<Child> Foo { get; set; }


        This presents a problem when you have two foreign keys to the same table. Since it does't use the
        property name, EF can't disambiguate the two references to the same class. Here's an example:


            public partial class Paper
            {
                public int Id { get; set; }
                public int CreatedByAuthorId { get; set; }
                public int UpdatedByAuthorId { get; set; }
                public string Name { get; set; }

                public virtual Author CreatedByAuthor { get; set; }
                public virtual Author UpdatedByAuthor { get; set; }
            }

            public partial class Author
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public List<Paper> Foos { get; set; }
            }


        We fix that by adding the [InverseProperty] attribute to the principal class:


            public partial class Author
            {
                public int Id { get; set; }
                public string Name { get; set; }

                [InverseProperty("CreatedByAuthor")]
                public List<Paper> CreatedPapers { get; set; }

                [InverseProperty("UpdatedByAuthor")]
                public List<Paper> UpdatedPapers { get; set; }
            }


        Aside from the above considerations, self-reference poses no special problems:


            public partial class Author
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public int? ManagerId { get; set; }

                public virtual Author Manager { get; set; }
                public List<Author> Manages { get; set; }
            }

                
***********************************************************************************************************************
***********************************************************************************************************************
***********************************************************************************************************************


        Let's look at One-to-One relationships. Here are some various schemas:

            author                              biographies
            --------           <1:0/1>          --------
            id (pk)            (<1:1>)          id (pk, fk)


            author                              biographies
            --------           <1:0/1>          --------
            id (pk)                             id (pk)
            bio_id (fk,null,unq)


            author                              biographies
            --------            <1:1>           --------
            id (pk)                             id (pk)
            bio_id (fk,unq)


        Note: in the last two examples, a unique index is used. Different RDBMSs behave differently
        here. For example, MySQL allows multiple nulls, where SQL Server does not. Oracle allows multiple
        nulls in a single index, but treats them as valued in composite unless all values are null. SQL
        Server 2008 does allow to work around this with "filtered indexes":

            CREATE UNIQUE INDEX myidx ON table(column) WHERE column IS NOT NULL;

        From EF's point of view, we are going to create a "Reference to Reference" relationship between
        two entities (no collections).
        
        There are two issues:

            Helping EF detirmine the primary entity
            Validation of 1:1 
            
        Note: again, 1:1 not always possible in the database, but EF can ensure the dependent entity 
        exists when saving.
            
        After setting up the reference navigation properties for the first schema:

            public partial class Biography
            {
                [Key] //note PK doesn't follow convention
                public int AuthorId { get; set; }
                public string Text { get; set; }

                public Author Author { get; set; }
            }
        
            public partial class Author
            {
                public int Id { get; set; }
                public string Name { get; set; }

                public Biography Biography { get; set; }
            }


        We will get the following error:

            "Unable to determine the principal end of an association between the types..."

        We can solve that by telling EF which is the primary entity by adding either of the
        following to the Biography class (the dependent entity):

            [Key,ForeignKey("Author")]
            public int AuthorId { get; set; }

        Or

            [Required]
            public virtual Author Author { get; set; }

        
        Since you cannot have a Biography without an Author, this gives EF the required info.

        You could turn the schema-define <1:0/1> into a <1:1> from EF's point of view by making
        both ends required (you must specify the FK). EF will validate that Authors always have
        a non-null Biography (even though it's not enforced in the database):

        While EF 6 does support the code-first creation of unique indexes:

            [Index("FOOIDX", 0, IsUnique = true)]

        It does not figure out the correct multiplicity for navigation properties based on unique
        contstraints. So the second and third schema examples are not strictly possible.


***********************************************************************************************************************
***********************************************************************************************************************
***********************************************************************************************************************


        Compound keys generally work "out of the box" as long as you specify the keys (and their order).
        You do that on the primary entity this way:

            public partial class Biography
            {
                ...

                [Key, Column("isbn", Order = 0)]
                public string Isbn { get; set; }

                [Key, Column("page_no", Order = 1)]
                public int PageNo { get; set; }

                [InverseProperty("Biography")]
                public virtual List<Author> Authors { get; set; }
            }

        And on the dependent entity this way:

            public partial class Author
            {
                ...

                [Column("bio_isbn")]
                public string BioIsbn { get; set; }

                [Column("bio_page_no")]
                public int? BioPageNo { get; set; }

                [ForeignKey("BioIsbn, BioPageNo")]
                public virtual Biography Biography { get; set; }
            }

        Or this way:

            public partial class Author
            {
                ...

                [Column("bio_isbn")]
                [ForeignKey("Biography")]
                public string BioIsbn { get; set; }

                [Column("bio_page_no")]
                [ForeignKey("Biography")]
                public int? BioPageNo { get; set; }

                public virtual Biography Biography { get; set; }
            }

        You can specify the order of the columns (ordnials do not have to match) in the dependent
        entity as well but EF shouldn't need that extra information.

        Note: we specify the column mappings here just because we chose non-conforming names.

        EF also supports "many-to-many" relationship directly on the entity without having to navigate 
        through the association table with a compound key. In other words, "collection to collection".

        We don't currently support this in the code generator, but a future version should.


    */

    public class EfGenerator:ClassGenerator
    {
        public EfGenerator(IClassWriter writer, IConvention convention, Dictionary<string, Table> tables, ClassOptions classOptions) : base(writer, convention, tables, classOptions)
        {
        }

        protected override string GetClassName(string tableName)
        {
            return EfContextGenerator.EfPrefix + base.GetClassName(tableName);
        }

        protected string _GetCollectionNavProperty(string tableName)
        {
            //user_types -> IList<EfUserType> UserTypes;
            return Convention.GetClassName(tableName, Pluralities.Plural);
        }

        protected string _GetReferenceNavPropertyFromTableName(string tableName)
        {
            //user_types -> EfUserType UserType;
            return Convention.GetClassName(tableName, Pluralities.Singular);
        }

        protected string _GetReferenceNavPropertyFromColumnName(string columnName)
        {
            //created_by_user_id => CreatedByUser
            //user_type_id => UserType

            columnName = Convention.GetReferenceName(columnName);
            return columnName;
        }

        protected override void OnGenerate(Table table, string className)
        {
            WriteLine("using System;");
            WriteLine("using System.Collections.Generic;");
            WriteLine("using System.ComponentModel.DataAnnotations.Schema;");

            //for debugging
            if (className == "EfClientCustomer" || className == "EfClient" || className == "EfUser")
            {
                int x = 1;
            }

            //bad hack
            if (ClassOptions.Namespace != null) WriteLine($"using {ClassOptions.Namespace};");

            WriteLine();

            if (ClassOptions.Namespace!=null) WriteLine($"namespace {ClassOptions.Namespace}{(ClassOptions.SubNamespace!=null?"."+ ClassOptions.SubNamespace:"")}\n{{");

            WriteLine($"\tpublic partial class {className}:{Convention.GetClassName(table.Name, Pluralities.Singular)}");
            WriteLine("\t{");

            //TODO to support multi-valued keys we would have to modify the Relationship class and the provider logic

            /*

                ForeignKeys
                    "on" table      -> this table              
                    "on" column     -> foreign key(s) in this table pointing to a primary or unique "other" column(s) in the "other" table
                
                References:
                    "other" table   -> table with a foreign key(s) pointing to this table, the "on" table
                    "other" column  -> foreign key(s) pointing to the "on" column(s)

                Note: again, we do not support multi-valued keys in EF relationships:

                    one-to-one with multi-valued keys
                    one-to-many with multi-valued keys
                    many-to-many

                We *do* support multi-valued primary keys, however, as in the compound keys of a many-to-many association table.

            TODO and multi-valued one-to-many?

            */

            
            //Generate fkey reference navigation properties for when this class is the dependent entity (single-valued keys only)
            foreach (Relationship fk in table.ForeignKeys.Values)
            {
                //is other table included in our table list (not 'skipped')?
                if ((from t in _tables.Values where t.Name==fk.OtherTable select t).Count()>0)
                {
                    /*
                        We're doing this in metadata now...

                        //created_by_user_id => [ForeignKey("CreatedByUserId")]
                        WriteLine($"\t\t[ForeignKey(\"{Convention.GetPropertyName(fk.OnColumn)}\")]");
                    */

                    /*
                        old dumb way

                        //created_by_user_id => public virtual EfUser CreatedByUser { get; set; }
                        //user_type_id => public virtual EfUserType UserType { get; set; }
                        string fkNav = GetReferenceNavPropertyFromColumnName(fk.OnColumn);
                        WriteLine($"\t\tpublic virtual {GetClassName(fk.OtherTable)} {fkNav} {{ get; set;}}");

                    */

                    //TODO push all this into GetReferenceName()
                    //single vs multi value FKs (same logic as in MetadataGenerator)
                    string fkNav;
                    if (fk.Columns.Count==1)
                    {
                        //for single-valued FKs, we can use the property name
                        //created_by_user_id => public virtual EfUser CreatedByUser { get; set; }
                        fkNav = Convention.GetReferenceName(fk.Columns[0].OnColumn);
                    }
                    else
                    {
                        //for multi-valued FKs, we should use the table name
                        //user_first_name, user_last_name => public virtual EfUser User { get; set; }
                        fkNav = Convention.GetClassName(fk.OtherTable, Pluralities.Singular);
                    }
                    WriteLine($"\t\tpublic virtual {GetClassName(fk.OtherTable)} {fkNav} {{ get; set;}}");

                }
            }

            //Generate collection or reference navigation properties for when this class is the primary entity (single-valued keys only)
            foreach (Relationship r in table.References.Values)
            {

                //is other table included in our table list (not 'skipped')?
                if ((from t in _tables.Values where t.Name == r.OtherTable select t).Count() > 0)
                {

                    //created_by_user_id => [InverseProperty("CreatedByUser")]
                    //TODO [0]
                    WriteLine($"\t\t[InverseProperty(\"{Convention.GetReferenceName(r.Columns[0].OtherColumn)}\")]");               

                    string entityNavPrefix = "";
                    /*
                        TODO remove?
                        what was the original intent of this? what use case?

                            //public virtual List<EfUser> CreatedByUser_Users { get; set; }
                            if (table.References.Where(tr => tr.OtherTable == r.OtherTable).Count() > 1)
                            {
                                entityNavPrefix = GetFKNavProperty(r.OtherColumn) + "_";
                            }

                    */

                    /*
                        Is the relationship "[on] <1:*> [other]"  or  "[on] <1:1> [other]"? 

                        TODO what we want:
                            1   test if the foreign keys (other columns) in the other table are also primary keys in that table 
                            2   test if the dual foreign/primary keys in the other table match our primary keys
                        
                        Until we support multi-valued keys, we're just going to
                            1   test if the foreign key (other column) is also a primary key
                            2   ensure the primary key in the other table is single-valued

                    */


                    //TODO push all this into GetCollectionName()
                    Table otherTable = _tables[r.OtherTable];
                    //TODO [0]
                    bool pFk = otherTable.Columns[r.Columns[0].OtherColumn].IsPrimary;                                 
                    int nPk = otherTable.Columns.Where(c => c.Value.IsPrimary == true).Count();

                    if (pFk && nPk==1)
                    {
                        //public virtual EfUser User { get; set; }
                        WriteLine($"\t\tpublic virtual {GetClassName(r.OtherTable)} {entityNavPrefix + Convention.GetClassName(r.OtherTable, Pluralities.Singular)} {{ get; set;}}");
                    }
                    else
                    {
                        //public virtual List<EfUser> Users { get; set; }
                        WriteLine($"\t\tpublic virtual List<{GetClassName(r.OtherTable)}> {entityNavPrefix + Convention.GetCollectionName(r.OtherTable)} {{ get; set;}}");
                    }


                }
            }


            WriteLine("\t}");
            if (ClassOptions.Namespace != null) WriteLine("}");



        }
    }
}
