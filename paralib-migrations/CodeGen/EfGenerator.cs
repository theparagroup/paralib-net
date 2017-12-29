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

            1] add the FK attribute to the FK property, pointing to the FK reference nav property

                    [ForeignKey("CreatedBy")]
                    public int CreatedByAuthorId { get; set; }


            2] add the FK attribute to the FK reference nav property, pointing to the FK property

                    [ForeignKey("CreatedByAuthorId")]
                    public Parent CreatedBy { get; set; }

***********************************************************************************************************************
***********************************************************************************************************************
***********************************************************************************************************************

        Let's add the inverse property (a collection in a one-to-many or a reference
        in a one-to-one) to the parent:


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

                [Column("bio_isbn", Order = 10)]
                [ForeignKey("Biography")]
                public string BioIsbn { get; set; }

                [Column("bio_page_no", Order = 11)]
                [ForeignKey("Biography")]
                public int? BioPageNo { get; set; }

                public virtual Biography Biography { get; set; }
            }

        Note: that in a one-to-one mapping, we can't use "the" column name of the FK to create the
        navigation property (since there are more than one columns in the FK). Instead we use the 
        name of the entity:

                public string CreatedByFirstName { get; set; }
                public string CreatedByLastName { get; set; }
                public virtual User User { get; set; }

        instead of:

                public string CreatedByUserId { get; set; }
                public virtual User CreatedByUser { get; set; }

        TODO: Support multiple multi-valued FKs to the same table (just number them):
                
                public virtual User User1 { get; set; }
                public virtual User User2 { get; set; }

        TODO: Or simply don't generate them at all and allow the developer to do so in a custom class.

        With a multi-valued FK, you must specify the order of the columns that comprise the key. The 
        ordnials do not have to match exactly but the ordering must be the same on the primary entity
        (see MetadataGenerator for more info on this). 

        Note: we specify the column mappings here just because we chose non-conforming names.

        TODO: EF also supports "many-to-many" relationship directly on the entity without having to navigate 
        through the association table with a compound key. In other words, "collection to collection". We don't 
        currently support this in the code generator, but a future version should.

        We do, of course, support many-to-many via an association table as coupled one-to-manys.

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

        protected override void OnGenerate(Table table, string className)
        {
            WriteLine("using System;");
            WriteLine("using System.Collections.Generic;");
            WriteLine("using System.ComponentModel.DataAnnotations.Schema;");

            //bad hack
            //TODO why?
            if (ClassOptions.Namespace != null) WriteLine($"using {ClassOptions.Namespace};");

            WriteLine();

            if (ClassOptions.Namespace!=null) WriteLine($"namespace {ClassOptions.Namespace}{(ClassOptions.SubNamespace!=null?"."+ ClassOptions.SubNamespace:"")}\n{{");

            WriteLine($"\tpublic partial class {className}:{Convention.GetClassName(table.Name, Pluralities.Singular)}");
            WriteLine("\t{");

            /*

                Navigation properties: virtual properties used to navigate relationships.
                Principal navigation property: the property on the primary key table that allows you to navigate to the table with the foreign key.
                Dependednet navigation property: the property on the foreign key table that allow you to nvaigation to the table the primary key.
                Reference property: a singled valued property. Principal properties are always reference properties, dependendants may be (one-to-one)
                Collection property: a multi-value property (collection). Dependent properties usually are these (one-to-many)

                Note: "Reference" can refer to "References" meaning "referring tables" (tables referring to this entity) or EF Reference Properties.


                ForeignKeys
                    "on" table      -> this table              
                    "on" column     -> foreign key(s) in this table pointing to a primary or unique "other" column(s) in the "other" table
                    "other" table   -> the table with the primary/unq key
                    "other" column  -> the primary/unq key
                
                References:
                    "on" table      -> this table              
                    "on" column     -> the column pointed to by a FK in the other table
                    "other" table   -> table with a foreign key(s) pointing to this table, the "on" table
                    "other" column  -> foreign key(s) pointing to the "on" column(s)


                We generate Principal Reference Navigation Properties for all the foriegn keys of this table first. They will be single valued as each
                foriegn key value points to a single EF entity:

                    Example 1:

                        [jobs] table
                            FK -> jobs.created_by_user_id

                        Job:
                            public long CreatedByUserId { get; set;}

                        EfJob:
                            public virtual EfUser CreatedByUser { get; set; } //nav property

                        Job Metadata:
            		        [ForeignKey("CreatedByUser")] //points to nav property above
            		        public object CreatedByUserId;

                    Example 2:

                        [user_info] table
                            PK, FK -> user_id

                        UserInfo:
                            public long UserId { get; set;} 

                        EfUserInfo
                            public virtual EfUser User { get; set; } //nav property

                        Job Metadata:
            		        [ForeignKey("User")] //points to above
            		        public object UserId;


                The FK Reference Navigation Property names are usually generated from the column name, which is descriptive. These names are used 
                in the Ef and metadata classes. You can change this with PrincipalNavigationProperty column metadata on the first column of a foreign key,
                you can specify the property name used when generating the Principal Reference Navigation properties on the primary table for the relationship.

                This will change the property name in the following places:

                    EfEntity class for principal entity (principal nav prop)
                    Metadata class for principal entity ([ForeignKey] attribute)
                    EfEntity class in the dependent entity ([InversePropertyAttribute])

                For example:

                    The metadata 
                        {"PrincipalNavigationProperty", "Creator"}
                    
                    Generates

                        EfJob:
                            public virtual EfUser Creator { get; set; } //nav property

                        Job Metadata:
            		        [ForeignKey("Creator")] //points to nav property above
            		        public object CreatedByUserId;

                        EfUser
                            [InverseProperty("Creator")] //must match nav property in principal entity
                            public virtual List<EfUser> Users { get; set; }


                Next we generate either Reference or Collection Navigation Properties for all the relationships where this table is the parent
                table in the relationship (we're the primary key and the other table has the foreign key). This relationship could be either
                a 1:1 or a 1:* where this table is always on a "1" side. In other words, the foreign key on the other table could also be the other
                table's primary key. A 1:1 Reference Property will look like this in the Ef entity class (using the examples from above):

                    //User Entity
                    [InverseProperty("User")] //must match nav property in principal entity
                    public virtual EfUser UserInfo { get; set; }

                A 1:* Collection Property looks like this:

                    //User Entity
                    [InverseProperty("CreatedByUser")] //must match nav property in principal entity
                    public virtual List<EfJob> Jobs { get; set; }

                These property names aren't very descriptive and must be unique, so when you have multiple FKs to the same table you need to specify
                the property name. By using "DependentNavigationProperty" column metadata on the first column of a foreign key, you can specify the property name used
                when generating the Dependent Reference/Collection Navigation properties on the primary table for the relationship.

                This will change the property name in the following places:

                    EfEntity class in the dependent entity (dependent navigation property - the one with the [InversePropertyAttribute])


                For example:

                    The metadata 
                        {"DependentNavigationProperty", "JobsCreatedByUser"}
                    
                    Generates

                        //User Entity
                        [InverseProperty("CreatedByUser")] //must match nav property in principal entity
                        public virtual List<EfJob> JobsCreatedByUser { get; set; }


            */

            int fkCount = 0;

            //Generate fkey navigation properties (Reference Properties) for when this class is the dependent entity
            foreach (Relationship fk in table.ForeignKeys.Values)
            {
                //is other table included in our table list (not 'skipped')?
                if ((from t in _tables.Values where t.Name==fk.OtherTable select t).Count()>0)
                {
                    /*

                        Note: we create [ForeignKey("table")] attributes that refer to this property in the MetadataGenerator.

                    */

                    //see if there is an "NavigationPropertyName" specified on the first column of the FK, if so, use that for the property name
                    string propertyName = GetExtendedProperty(fk.OnTable, fk.Columns[0].OnColumn, nameof(ExtendedProperties.PrincipalNavigationProperty));

                    //single vs multi valued keys
                    //created_by_user_id => public virtual EfUser CreatedByUser { get; set; }
                    //user_first_name, user_last_name => public virtual EfUser User { get; set; }
                    WriteLine($"\t\tpublic virtual {GetClassName(fk.OtherTable)} {propertyName??MetadataGenerator.GetReferenceName(Convention, fk)} {{ get; set;}}");

                    ++fkCount;
                }
            }

            bool lineBreak = false;
            bool firstProperty = false;

            //Generate navigation properties (Collections or References) for when this class is the primary entity
            //note
            foreach (Relationship r in table.References.Values)
            {

                //is other table included in our table list (not 'skipped')?
                if ((from t in _tables.Values where t.Name == r.OtherTable select t).Count() > 0)
                {
                    Table otherTable = _tables[r.OtherTable];

                    //put a line break between references and inverses
                    if (!lineBreak && fkCount>0)
                    {
                        WriteLine();
                        lineBreak = true;
                    }

                    //see if there is an "NavigationPropertyName" specified on the other column, if so, use that for the property name
                    string primaryPropertyName = GetExtendedProperty(otherTable.Columns[r.Columns[0].OtherColumn].Properties, nameof(ExtendedProperties.PrincipalNavigationProperty));

                    //link to the dependent entity's reference property
                    //created_by_user_id => [InverseProperty("CreatedByUser")]
                    //user_first_name, user_last_name => [InverseProperty("User")]

                    if (firstProperty)
                    {
                        WriteLine();
                    }

                    firstProperty = true;

                    WriteLine($"\t\t[InverseProperty(\"{primaryPropertyName??MetadataGenerator.GetReferenceName(Convention, r, true)}\")]");

                    //see if there is an "InversePropertyName" specified on the other column, if so, use that for the property name
                    string propertyName = GetExtendedProperty(otherTable.Columns[r.Columns[0].OtherColumn].Properties, nameof(ExtendedProperties.DependentNavigationProperty));

                    /*

                        Is the relationship "[on] <1:*> [other]"  or  "[on] <1:1> [other]"? 

                        what we want:
                            1   test if the foreign keys (other columns) in the other table are also primary keys in that table 
                            2   test if the dual foreign/primary keys in the other table match our primary keys

                    */


                    bool oneToOne = false;

                    if (r.Columns.Count == otherTable.PrimaryKeys.Count)
                    {
                        oneToOne = true;

                        for (int i = 0; i < r.Columns.Count; ++i)
                        {
                            if (r.Columns[i].OtherColumn != otherTable.PrimaryKeys[i].Name)
                            {
                                oneToOne = false;
                            }
                        }

                    }

                    if (oneToOne)
                    {
                        //public virtual EfUser User { get; set; }
                        WriteLine($"\t\tpublic virtual {GetClassName(r.OtherTable)} {propertyName??Convention.GetClassName(r.OtherTable, Pluralities.Singular)} {{ get; set;}}");
                    }
                    else
                    {
                        //public virtual List<EfUser> Users { get; set; }
                        WriteLine($"\t\tpublic virtual List<{GetClassName(r.OtherTable)}> {propertyName??Convention.GetClassName(r.OtherTable, Pluralities.Plural)} {{ get; set;}}");
                    }

                }
            }


            WriteLine("\t}");
            if (ClassOptions.Namespace != null) WriteLine("}");



        }
    }
}
