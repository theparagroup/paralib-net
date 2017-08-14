using System;
using System.Linq;
using System.Collections.Generic;
using com.paralib.Dal.Metadata;
using com.paralib.Dal.Utils;

namespace com.paralib.Migrations.CodeGen
{
    /*
        The ModelGenerator generates a basic POCO in the root of the model assembly. We don't use 
        EntityObject or IPOCO.

        EF uses properties with getters and setters to map data to a database. It is also possible to configure
        "backing fields" (via fluent) which will bypass the setter when the data is populated.

         We use the "paralib convention" which is 

            table name              class name
            -----------------       -----------------
            customer_addresses      EfCustomerAddress


            column name             property name
            -----------------       -----------------
            state_id                StateId


        You can exclude properties from mapping with:

            [NotMapped]

        The conventions can be overridden with:

            [Table("States")]
            [Column("StateID")]


        EF will generate dynamic proxies for lazy loading of navigation properties, as well as more-efficient
        change tracking. Here we could generate all of the properties as "virtual" to support the better
        change tracking, but we currently do not. Both kinds of proxy generation can be disabled.

        The models need to be partial because we spread the generation out over several files:

            Model       -> simple POCOs
            Logic       -> empty class that is not regenerated where you can put custom logic
            Metadata    -> general metadata (data validation, keys, order, etc)
            Ef          -> Derived classes that contain the EF navigation properties are included in the DbContext
            Nh          -> Derived class for use with NHibernate (future) 



        Basic example of model classes:

            public partial class Parent
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }


            public partial class Child
            {
                public int Id { get; set; }
                public int ParentId { get; set; }
                public string Name { get; set; }
            }


    */

    public class ModelGenerator:ClassGenerator
    {

        public ModelGenerator(IClassWriter writer, IConvention convention, Dictionary<string, Table> tables, ClassOptions classOptions) : base(writer, convention, tables, classOptions)
        {
        }

        protected override void OnGenerate(Table table, string className)
        {
            WriteLine("using System;");
            WriteLine();

            if (ClassOptions.Namespace!=null) WriteLine($"namespace {ClassOptions.Namespace}{(ClassOptions.SubNamespace!=null?"."+ ClassOptions.SubNamespace:"")}\n{{");

            string classSig = $"\tpublic partial class {className}";
            if (ClassOptions.Implements != null) classSig += $":{ClassOptions.Implements}";
            WriteLine(classSig);
            WriteLine("\t{");

            if (ClassOptions.Ctor != null) WriteLine($"\t\tpublic {className}{ClassOptions.Ctor} {{}}\n");

            foreach (Column column in table.Columns.Values)
            {
                WriteLine($"\t\tpublic {CSharpTypes.GetKeyword(column.ClrType)}{(IsNullable(column)?"?":"")} {Convention.GetPropertyName(column.Name)} {{ get; set;}}");
            }

            WriteLine("\t}");
            if (ClassOptions.Namespace != null) WriteLine("}");



        }
    }
}
