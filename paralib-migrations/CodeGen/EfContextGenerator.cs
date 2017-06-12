using System;
using System.Linq;
using com.paralib.Dal.Metadata;
using com.paralib.Dal.Utils;
using com.paralib.Ado;
using com.paralib.Dal;

namespace com.paralib.Migrations.CodeGen
{

    public class EfContextGenerator:Generator
    {
        public EfContextGenerator(IClassWriter writer, IConvention convention, string[] skip, ClassOptions classOptions) : base(writer, convention, skip, classOptions)
        {
        }

        protected override string GetClassName(string tableName)
        {
            return Convention.EfPrefix + base.GetClassName(tableName);
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

            Table[] tables = null;

            using (var db = new Db(database))
            {
                tables = db.GetTables();
            }

            foreach (Table table in tables)
            {
                if (_skip != null && (from s in _skip where s == table.Name select s).Count() > 0) continue;

                //public DbSet<EfUser> Users { get; set; }
                WriteLine($"\t\tpublic DbSet<{GetClassName(table.Name)}> {Convention.GetClassName(table.Name,Pluralities.Plural)} {{ get; set; }}");

            }

            WriteLine("\t}");
            if (ClassOptions.Namespace != null) WriteLine("}");

            End();



        }
    }
}
