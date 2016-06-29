using System;
using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using com.paralib.Ado;

namespace com.paralib.Migrations.Runner
{
    public class RunnerFactory
    {

        public static MigrationRunner Create(Assembly assembly, MigrationOptions migrationOptions, Database database, Announcer announcer=null)
        {
            announcer = announcer ?? new ConsoleAnnouncer();
            var migrationContext = new RunnerContext(announcer);

            MigrationProcessorFactory factory=null;

            switch (database.DatabaseType)
            {
                case DatabaseTypes.SqlServer:
                    factory = new FluentMigrator.Runner.Processors.SqlServer.SqlServer2008ProcessorFactory();
                    break;

                case DatabaseTypes.MySql:
                    factory = new FluentMigrator.Runner.Processors.MySql.MySqlProcessorFactory();
                    break;
            }

            var processor = factory.Create(database.GetConnectionString(true), announcer, migrationOptions);
            var migrator = new MigrationRunner(assembly, migrationContext, processor);


            return migrator;
        }


    }
}
