using System;
using System.Reflection;
using NET=System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Text.RegularExpressions;

namespace com.paralib.Dal.Ef
{
    public class DbContext : NET.DbContext
    {

        public DbContext(): base(Paralib.Configuration.ConnectionString)
        {
            NET.Database.SetInitializer<DbContext>(null);
        }


        protected override void OnModelCreating(NET.DbModelBuilder modelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention
            // If you keep this convention, the generated tables 
            // will have pluralized names.
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var c = modelBuilder.Conventions;

            modelBuilder.Types()
            .Configure(config => config.ToTable(GetTableName(config)));


            modelBuilder.Properties()
            .Configure(config => config.HasColumnName(GetColumnName(config.ClrPropertyInfo)));

        }

        private static string GetTableName(ConventionTypeConfiguration config)
        {
            var result = Regex.Replace(config.ClrType.Name, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]);
            return result.ToLower() + "s";
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
