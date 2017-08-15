using System;
using System.Linq;
using System.Collections.Generic;
using com.paralib.Dal.Metadata;
using com.paralib.Dal.Utils;
using com.paralib.Ado;
using com.paralib.Dal;

namespace com.paralib.Migrations.CodeGen
{
    /*
        Generates the DbContext class as a sub-class of the DAL's EfContext class, which 
        changes various settings (initializer, naming conventions) from the Microsoft default, supports
        our paralib logging, DAL databases, SQL logging, etc.
        
        The naming conventions in the EfContext are hardcoded and are currently the mirror image of the 
        ParalibConvention used when generating the model and their related classes:

            database name           class name
            -----------------       -----------------
            customer_addresses      EfCustomerAddress

        Later versions should support other conventions (Microsoft default, other custom), or perhaps
        we would made full-explicit-declaration ([Table],[Column]) an option.


    */

    public class EfContextGenerator:Generator
    {
        public const string EfPrefix = "Ef";

        public EfContextGenerator(IClassWriter writer, IConvention convention, Dictionary<string, Table> tables, ClassOptions classOptions) : base(writer, convention, tables, classOptions)
        {
        }

        protected override string GetClassName(string tableName)
        {
            return EfPrefix + base.GetClassName(tableName);
        }

        public void Generate(Database database)
        {
            Start("DbContext");
            WriteLine("using System;");
            WriteLine("using System.Data.Entity;");
            WriteLine("using com.paralib.Dal.Ef;");
            WriteLine("using para=com.paralib.Ado;");
            WriteLine();

            if (ClassOptions.Namespace != null) WriteLine($"namespace {ClassOptions.Namespace}{(ClassOptions.SubNamespace != null ? "." + ClassOptions.SubNamespace : "")}\n{{");

            WriteLine("\t[DbConfigurationType(typeof(EfConfiguration))]");

            WriteLine($"\tpublic partial class DbContext:EfContext");
            WriteLine("\t{");

            WriteLine();
            WriteLine("\t\tpublic DbContext() { _init(); }");
            WriteLine("\t\tpublic DbContext(string connectionString) : base(connectionString) { _init(); }");
            WriteLine("\t\tpublic DbContext(para.Database database) : base(database) { _init(); }");
            WriteLine();

            WriteLine("\t\tprivate void _init()");
            WriteLine("\t\t{");
            WriteLine("\t\t\t OnInit();");
            WriteLine("\t\t}");
            WriteLine();

            //diagnostics
            WriteLine("\t\tprotected virtual void OnInit()");
            WriteLine("\t\t{");
            WriteLine("\t\t\t#if DEBUG");
            WriteLine("\t\t\tDatabase.Log = message => System.Diagnostics.Debug.WriteLine(message);");
            WriteLine("\t\t\t#endif");
            WriteLine("\t\t}");
            WriteLine();


            foreach (Table table in _tables.Values)
            {

                //public DbSet<EfUser> Users { get; set; }
                WriteLine($"\t\tpublic DbSet<{GetClassName(table.Name)}> {Convention.GetClassName(table.Name,Pluralities.Plural)} {{ get; set; }}");

            }

            WriteLine("\t}");
            if (ClassOptions.Namespace != null) WriteLine("}");

            End();



        }
    }
}
