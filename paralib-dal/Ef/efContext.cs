using System;
using System.Reflection;
using NET = System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Text.RegularExpressions;
using com.paralib.Ado;
using com.paralib.Dal.Utils;

namespace com.paralib.Dal.Ef
{
    [NET.DbConfigurationType(typeof(EfConfiguration))]
    public class EfContext : NET.DbContext
    {
        private static ILog _log = Paralib.GetLogger(typeof(EfContext));

        public EfContext(string connectionString) : base(connectionString)
        {
        }

        public EfContext(Database database) : this(CheckDatabase(database).GetConnectionString(true))
        {
            _log.Info($"Setting connection to [{database.Name}] = '{database.GetConnectionString(false)}'");
        }

        private static Database CheckDatabase(Database database)
        {
            if (database == null) throw new ParalibException("Database cannot be null.");
            return database;
        }

        public EfContext(): this(Paralib.Dal.Database)
        {
            _log.Info($"Setting connection to default database.");

            //Note: this won't work for derived classes:
            //      NET.Database.SetInitializer<EfContext>(null);
            //
            //so let's use reflection to invoke this member which for some reason is generic. yes.
            _log.Info($"Disabling EF database initialization.");
            var databaseType = typeof(NET.Database);
            var setInitializer = databaseType.GetMethod("SetInitializer", BindingFlags.Static | BindingFlags.Public);
            var thisType = GetType();
            var setInitializerT = setInitializer.MakeGenericMethod(thisType);
            setInitializerT.Invoke(null, new object[] { null });

        }


        protected override void OnModelCreating(NET.DbModelBuilder modelBuilder)
        {

            // Configure Code First to ignore PluralizingTableName convention
            // If you keep this convention, the generated tables 
            // will have pluralized names.
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            _log.Info($"Adding Parlib EF naming conventions.");

            var c = modelBuilder.Conventions;

            modelBuilder.Types()
            .Configure(config => config.ToTable(GetTableName(config)));


            modelBuilder.Properties()
            .Configure(config => config.HasColumnName(GetColumnName(config.ClrPropertyInfo)));

        }

        private static string GetTableName(ConventionTypeConfiguration config)
        {
            //EfEmployeeType -> employee_types

            var result = Regex.Replace(config.ClrType.Name, "^Ef", m => "");
            result = Regex.Replace(result, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]);

            string[] parts = result.Split('_');
            parts[parts.Length - 1] = Lexeme.Pluralize(parts[parts.Length - 1]);
            result = string.Join("_", parts);

            result = result.ToLower();

            return result;
          
        }


        private static string GetColumnName(PropertyInfo property)
        {
            var result = Regex.Replace(property.Name, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]);
            //var result = property.DeclaringType.Name + "_";
            //result += Regex.Replace(property.Name,".[A-Z]", m => m.Value[0] + "_" + m.Value[1]);
            return result.ToLower();
        }


    }
}
