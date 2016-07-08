using System;
using System.Linq;
using com.paralib.Dal.Metadata;
using com.paralib.Dal.Utils;
using System.Text.RegularExpressions;

namespace com.paralib.Migrations.CodeGen
{

    public class EfGenerator:ClassGenerator
    {
        public EfGenerator(IClassWriter writer, IConvention convention, string[] skip, ClassOptions classOptions) : base(writer, convention, skip, classOptions)
        {
        }

        protected override string GetClassName(string tableName)
        {
            return Convention.EfPrefix + base.GetClassName(tableName);
        }

        protected override void OnGenerate(Table table, string className)
        {
            WriteLine("using System;");
            WriteLine("using System.Collections.Generic;");

            //bad hack
            if (ClassOptions.Namespace != null) WriteLine($"using {ClassOptions.Namespace};");

            WriteLine();

            if (ClassOptions.Namespace!=null) WriteLine($"namespace {ClassOptions.Namespace}{(ClassOptions.SubNamespace!=null?"."+ ClassOptions.SubNamespace:"")}\n{{");

            WriteLine($"\tpublic class {className}:{Convention.GetClassName(table.Name,true)}");
            WriteLine("\t{");

            foreach  (Relationship r in table.ForeignKeys)
            {
                //OnColumn = created_by_user_id => public virtual EfUser CreatedByUser { get; set; }
                //OnColumn = user_type_id => public virtual EfUserType UserType { get; set; }
                string onColumn = Regex.Replace(r.OnColumn.ToLower(), "_id$", m => "");


                WriteLine($"\t\tpublic virtual {GetClassName(r.OtherTable)} {Convention.GetClassName(onColumn, true)} {{ get; set;}}");
            }

            foreach (Relationship r in table.References)
            {
                //public virtual List<EfUser> Users { get; set; }
                WriteLine($"\t\tpublic virtual List<{GetClassName(r.OtherTable)}> {Convention.GetClassName(r.OtherTable, false)} {{ get; set;}}");
            }


            WriteLine("\t}");
            if (ClassOptions.Namespace != null) WriteLine("}");



        }
    }
}
