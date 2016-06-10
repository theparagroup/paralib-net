using System;
using System.Configuration;

namespace com.paralib.common.Configuration
{
    public class ConfigurationManager
    {



        public static void Configure(System.Configuration.Configuration cfg)
        {


            
            if (cfg.Sections["paralib"] == null)
            {
                var paralibSection = new ParalibSection();
                cfg.Sections.Add("paralib", paralibSection);
                paralibSection.SectionInformation.ForceSave = true;

                paralibSection.Dal = new DalElement();
                paralibSection.Dal.Connection = "con1";

                //customSection.Elements.Add(new CustomElement("1"));
                //customSection.Elements.Add(new CustomElement("2"));

                //cfg.Save(ConfigurationSaveMode.Full);
            }


            cfg.Save();


        }

        public static void Configure()
        {

        }

        public static void WriteConnectionString(System.Configuration.Configuration cfg)
        {
            //EXAMPLE
            ConnectionStringsSection connections = (ConnectionStringsSection)cfg.GetSection("connectionStrings");

            if (connections.ConnectionStrings["dal"] == null)
            {
                connections.ConnectionStrings.Add(new ConnectionStringSettings("dal", "Data Source=.\\SQLEXPRESS;Initial Catalog={database};Integrated Security=True;"));
            }

        }
    }







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







}
