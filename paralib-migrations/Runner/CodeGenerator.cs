using System;
using com.paralib.Ado;
using com.paralib.Migrations.CodeGen;
using com.paralib.Dal;
using com.paralib.Dal.Metadata;
using com.paralib.ParalibProperties;

namespace com.paralib.Migrations.Runner
{
    public class CodeGenerator
    {

       

        public static void Generate(Database database)
        {

            //models
            Generate(typeof(ModelGenerator), null, null, database, Paralib.Migrations.Codegen.Model);

            //logic
            Generate(typeof(LogicGenerator), "Logic", null, database, Paralib.Migrations.Codegen.Logic);

            //metadata
            Generate(typeof(MetadataGenerator), "Metadata", "Models", database, Paralib.Migrations.Codegen.Metadata, "Metadata");

            //ef

            //nh

        }

        public static void Generate(Type generatorType, string defaultSubDirectory, string defaultSubNamespace, Database database, GenerationProperties properties, string parameter = null)
        {
            if (properties.Enabled)
            {
                FileOptions fo = GetFileOptions(properties, defaultSubDirectory);
                ClassOptions co = GetClassOptions(properties, defaultSubNamespace, parameter);
                IConvention convention = GetConvention();

                Generator generator = (Generator)Activator.CreateInstance(generatorType, new FileWriter(fo), convention, Paralib.Migrations.Codegen.Skip, co);

                Table[] tables = null;

                using (var db = new Db())
                {
                    tables = db.GetTables();
                }

                generator.Generate(tables);
            }
        }

        protected static FileOptions GetFileOptions(GenerationProperties properties, string defaultSubDirectory)
        {
            return new FileOptions() { Path = properties.Path ?? Paralib.Migrations.Codegen.Path, SubDirectory=(properties.Path==null?defaultSubDirectory: null), Replace = properties.Replace };
        }

        protected static ClassOptions GetClassOptions(GenerationProperties properties, string defaultSubNamespace, string parameter)
        {
            return new ClassOptions() { Namespace = properties.Namespace ?? Paralib.Migrations.Codegen.Namespace, SubNamespace = (properties.Path == null ? defaultSubNamespace : null), Parameter = parameter, Implements = properties.Implements, Ctor = properties.Ctor };
        }

        protected static IConvention GetConvention()
        {
            string conventionName = Paralib.Migrations.Codegen.Convention;
            IConvention convention = null;

            if (conventionName != null)
            {
                if (conventionName == "Paralib")
                {
                    convention = new CodeGen.Conventions.Paralib();
                }
                else if (conventionName == "Microsoft")
                {
                    convention = new CodeGen.Conventions.Microsoft();
                }
                else
                {
                    convention = (IConvention)Activator.CreateInstance(Type.GetType(conventionName));
                }
            }
            else
            {
                convention = new CodeGen.Conventions.Paralib();
            }

            convention.Implements = Paralib.Migrations.Codegen.Model.Implements ?? convention.Implements;
            convention.Ctor = Paralib.Migrations.Codegen.Model.Implements ?? convention.Ctor;

            return convention;
        }
    }
}
