using System;
using com.paralib.Ado;
using com.paralib.Migrations.CodeGen;
using com.paralib.Dal;
using com.paralib.Dal.Metadata;
using com.paralib.ParalibProperties;
using System.Collections.Generic;

namespace com.paralib.Migrations.Runner
{
    public class CodeGenerator
    {

        protected static Dictionary<string, Table> GetTables(Database database, string[] skip)
        {
            //TODO this is currently blacklist with wildcard, it would be handy to whitelist as well

            Dictionary<string, Table> tables;

            using (var db = new Db(database))
            {
                tables = db.GetTables();
            }

            List<string> skipList = new List<string>();

            if (skip != null)
            {

                foreach (string tableName in tables.Keys)
                {
                    foreach (var s in skip)
                    {
                        if (s.EndsWith("*"))
                        {
                            if (tableName.StartsWith(s.Substring(0, s.Length - 1)))
                            {
                                skipList.Add(tableName);
                            }
                        }
                        else
                        {
                            if (s == tableName)
                            {
                                skipList.Add(tableName);
                            }
                        }
                    }

                }

                foreach (string tableName in skipList)
                {
                    tables.Remove(tableName);
                }

            }

            return tables;
        }

        public static void Generate(Database database)
        {
            //TODO refactor this to not use type activiation (makes refactoring a pain)

            //get tables
            Dictionary<string, Table> tables = GetTables(database, Paralib.Migrations.Codegen.Skip);

            //models
            Generate(typeof(ModelGenerator), null, null, database, tables, Paralib.Migrations.Codegen.Model);

            //logic
            Generate(typeof(LogicGenerator), "Logic", null, database, tables, Paralib.Migrations.Codegen.Logic);

            //metadata
            Generate(typeof(MetadataGenerator), "Metadata", null, database, tables, Paralib.Migrations.Codegen.Metadata, "Metadata");

            //ef
            Generate(typeof(EfGenerator), "Ef", "Ef", database, tables, Paralib.Migrations.Codegen.Ef);

            //EfContext or DbContext depending on convention
            if (Paralib.Migrations.Codegen.Ef.Enabled)
            {
                new EfContextGenerator(new ClassFileWriter(GetFileOptions(Paralib.Migrations.Codegen.Ef, "Ef")), GetConvention(), tables, GetClassOptions(Paralib.Migrations.Codegen.Ef, "Ef")).Generate(database);
            }


            //TODO nh

        }

        public static void Generate(Type generatorType, string defaultSubDirectory, string defaultSubNamespace, Database database, Dictionary<string, Table> tables, GenerationProperties properties, string parameter = null)
        {
            if (properties.Enabled)
            {
                FileOptions fo = GetFileOptions(properties, defaultSubDirectory);
                ClassOptions co = GetClassOptions(properties, defaultSubNamespace, parameter);
                IConvention convention = GetConvention();

                ClassGenerator generator = (ClassGenerator)Activator.CreateInstance(generatorType, new ClassFileWriter(fo), convention, tables, co);

                generator.Generate();
            }
        }

        protected static FileOptions GetFileOptions(GenerationProperties properties, string defaultSubDirectory)
        {
            return new FileOptions() { Path = properties.Path ?? Paralib.Migrations.Codegen.Path, SubDirectory=(properties.Path==null?defaultSubDirectory: null), Replace = properties.Replace };
        }

        protected static ClassOptions GetClassOptions(GenerationProperties properties, string defaultSubNamespace, string parameter=null)
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
                    convention = new CodeGen.Conventions.ParalibConvention();
                }
                else if (conventionName == "Microsoft")
                {
                    convention = new CodeGen.Conventions.MicrosoftConvention();
                }
                else
                {
                    convention = (IConvention)Activator.CreateInstance(Type.GetType(conventionName));
                }
            }
            else
            {
                convention = new CodeGen.Conventions.ParalibConvention();
            }

            convention.Implements = Paralib.Migrations.Codegen.Model.Implements ?? convention.Implements;
            convention.Ctor = Paralib.Migrations.Codegen.Model.Implements ?? convention.Ctor;

            return convention;
        }
    }
}
