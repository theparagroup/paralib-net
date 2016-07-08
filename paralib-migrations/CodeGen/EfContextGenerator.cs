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
            WriteLine();

            if (ClassOptions.Namespace != null) WriteLine($"namespace {ClassOptions.Namespace}{(ClassOptions.SubNamespace != null ? "." + ClassOptions.SubNamespace : "")}\n{{");

            WriteLine("\t[DbConfigurationType(typeof(EfConfiguration))]");
            WriteLine($"\tpublic class DbContext:EfContext");
            WriteLine("\t{");

            //diagnostics
            WriteLine();
            WriteLine("#if DEBUG");
            WriteLine("\t\tpublic DbContext()");
            WriteLine("\t\t{");
            WriteLine("\t\t\tDatabase.Log = message => System.Diagnostics.Debug.WriteLine(message);");
            WriteLine("\t\t}");
            WriteLine("#endif");
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
                WriteLine($"\t\tpublic DbSet<{GetClassName(table.Name)}> {Convention.GetClassName(table.Name,false)} {{ get; set; }}");

            }

            WriteLine("\t}");
            if (ClassOptions.Namespace != null) WriteLine("}");

            End();



        }
    }
}
