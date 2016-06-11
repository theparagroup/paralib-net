using System;
using NET=System.Configuration;

namespace com.paralib.common.Configuration
{
    public class ConfigurationManager
    {



        public static void CreateParalibSection(NET.Configuration cfg)
        {
            
            if (cfg.Sections["paralib"] == null)
            {
                var paralibSection = new ParalibSection();
                cfg.Sections.Add("paralib", paralibSection);
                paralibSection.SectionInformation.ForceSave = true;

                paralibSection.Dal = new DalElement();
                paralibSection.Dal.Connection = "con1";



                CreateConnectionString(cfg,"con1", "Data Source=.\\SQLEXPRESS;Initial Catalog={database};Integrated Security=True;");


                //customSection.Elements.Add(new CustomElement("1"));
                //customSection.Elements.Add(new CustomElement("2"));

            }




        }


        public static ParalibSection GetParalibSection()
        {
            return (ParalibSection) NET.ConfigurationManager.GetSection("paralib");
        }


        public static void CreateConnectionString(System.Configuration.Configuration cfg, string name, string connectionString)
        {
            NET.ConnectionStringsSection connections = (NET.ConnectionStringsSection)cfg.GetSection("connectionStrings");

            if (connections.ConnectionStrings[name] == null)
            {
                connections.ConnectionStrings.Add(new NET.ConnectionStringSettings(name, connectionString));
            }

        }

        public static string GetConnectionString(string name)
        {
            NET.ConnectionStringSettings connectionStringSettings = NET.ConfigurationManager.ConnectionStrings[name];

            if (connectionStringSettings != null)
            {
                return connectionStringSettings.ConnectionString;
            }
            else
            {
                return null;
            }

        }
    }







  






}
