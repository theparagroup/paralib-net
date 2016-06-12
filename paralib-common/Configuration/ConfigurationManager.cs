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

                //off by default
                paralibSection.Enabled = false;

                paralibSection.Dal = new DalElement();
                paralibSection.Dal.Connection = "con1";


                //add section
                cfg.Sections.Add("paralib", paralibSection);
                paralibSection.SectionInformation.ForceSave = true;

                //add "starter" connectionstring
                CreateConnectionString(cfg,"con1", "Data Source=.\\SQLEXPRESS;Initial Catalog={database};Integrated Security=True;");


                //customSection.Elements.Add(new CustomElement("1"));
                //customSection.Elements.Add(new CustomElement("2"));

            }




        }


        public static void Save(System.Configuration.Configuration cfg, bool full=false)
        {

            if (full)
            {
                //this is interesting as it will "dump" the entire configuration, which is quite large.
                cfg.Save(NET.ConfigurationSaveMode.Full, true);
            }
            else
            {
                cfg.Save();
            }


        }


        public static void CreateConnectionString(System.Configuration.Configuration cfg, string name, string connectionString)
        {
            NET.ConnectionStringsSection connections = (NET.ConnectionStringsSection)cfg.GetSection("connectionStrings");

            if (connections.ConnectionStrings[name] == null)
            {
                connections.ConnectionStrings.Add(new NET.ConnectionStringSettings(name, connectionString));
            }

        }


        public static ParalibSection GetParalibSection()
        {
            return (ParalibSection) NET.ConfigurationManager.GetSection("paralib");
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



    /*

          public class CustomSection : ConfigurationSection
        {
            [ConfigurationProperty("Elements")]
            //[ConfigurationCollection(typeof(NameValueElementCollection), AddItemName =“entry”)]
            public CustomElementCollection Elements
            {
                get { return ((CustomElementCollection)(base["Elements"])); }
                set { base["Elements"] = value; }
            }

        }


        [ConfigurationCollection(typeof(CustomElement))]
        public class CustomElementCollection : ConfigurationElementCollection
        {

            public void Add(CustomElement element)
            {
                BaseAdd(element);
            }

            protected override ConfigurationElement CreateNewElement()
            {
                return new CustomElement();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((CustomElement)(element)).Id;
            }


        }


        public class CustomElement : ConfigurationElement
        {

            public CustomElement()
            {
            }

            public CustomElement(string id)
            {
                Id = id;
            }

            [ConfigurationProperty("Id", IsRequired = true, IsKey = true)]
            public String Id
            {
                get { return (String)base["Id"]; }
                set { base["Id"] = value; }
            }

            [ConfigurationProperty("LastName", IsRequired = true, DefaultValue = "TEST")]
            public String LastName
            {
                get { return (String)base["LastName"]; }
                set { base["LastName"] = value; }
            }

            [ConfigurationProperty("FirstName", IsRequired = true, DefaultValue = "TEST")]
            public String FirstName
            {
                get { return (String)base["FirstName"]; }
                set { base["FirstName"] = value; }
            }
        }

        */










}
