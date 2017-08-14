using System;
using System.Linq;
using System.Collections.Generic;
using com.paralib.Dal.Metadata;
using com.paralib.Dal.Utils;

namespace com.paralib.Migrations.CodeGen
{


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
