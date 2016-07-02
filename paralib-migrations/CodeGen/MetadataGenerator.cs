using System;
using System.Linq;
using com.paralib.Dal.Metadata;

namespace com.paralib.Migrations.CodeGen
{


    public class MetadataGenerator:ClassGenerator
    {

        public MetadataGenerator(IClassWriter writer, IConvention convention, string[] skip, ClassOptions classOptions) : base(writer, convention, skip, classOptions)
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

                //key info (useful for EF on non-standard columns
                if (column.IsPrimary) WriteLine($"\t\t[Key, Column(Order = {i})]");

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
