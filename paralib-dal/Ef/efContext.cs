using System;
using System.Reflection;
using NET = System.Data.Entity;
using System.Data.Entity.Design.PluralizationServices;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Text.RegularExpressions;
using System.Globalization;

namespace com.paralib.Dal.Ef
{
    public class EfContext : NET.DbContext
    {

        public EfContext(): base(Paralib.Configuration.ConnectionString)
        {
            //Note: this won't work for derived classes:
            //      NET.Database.SetInitializer<EfContext>(null);

            //so let's use reflection to invoke this member which for some reason is generic. yes.
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

            var c = modelBuilder.Conventions;

            modelBuilder.Types()
            .Configure(config => config.ToTable(GetTableName(config)));


            modelBuilder.Properties()
            .Configure(config => config.HasColumnName(GetColumnName(config.ClrPropertyInfo)));

        }

        private static string GetTableName(ConventionTypeConfiguration config)
        {
            var result = Regex.Replace(config.ClrType.Name, "^Ef", m => "");
            result = Regex.Replace(result, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]);

            var p = PluralizationService.CreateService(new CultureInfo("en-US"));


            string s;

            s = p.Pluralize("company");
            s = p.Pluralize("labor");
            s = p.Pluralize("alumnus");
            s = p.Pluralize("fungus");

            result = p.Pluralize(result);
            return result;

            //if (result.EndsWith("y",StringComparison.OrdinalIgnoreCase))
            //{
            //    //Ending in 'y' preceded by a consonant -> ies
            //    return result.ToLower() + "ies";
            //}
            //else if (result.EndsWith("s", StringComparison.OrdinalIgnoreCase))
            //{
            //    //s, x, z, ch, or sh -> es
            //    return result.ToLower() + "es";
            //}
            //else
            //{
            //    //Ending in 'y' preceded by a vowel -> s
            //    return result.ToLower() + "s";
            //}
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
