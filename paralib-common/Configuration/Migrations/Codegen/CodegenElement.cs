using System;
using System.Configuration;

namespace com.paralib.Configuration.Migrations.Codegen
{

    public class CodegenElement : ConfigurationElement
    {

        [ConfigurationProperty("path")]
        public string Path
        {
            get { return (string)base["path"]; }
            set { base["path"] = value; }
        }

        [ConfigurationProperty("namespace")]
        public string Namespace
        {
            get { return (string)base["namespace"]; }
            set { base["namespace"] = value; }
        }

        [ConfigurationProperty("skip")]
        [ConfigurationCollection(typeof(SkipCollection), AddItemName = "table")]
        public SkipCollection Skip
        {
            get { return ((SkipCollection)(base["skip"])); }
            set { base["skip"] = value; }
        }

        [ConfigurationProperty("convention")]
        public string Convention
        {
            get { return (string)base["convention"]; }
            set { base["convention"] = value; }
        }


        [ConfigurationProperty("model")]
        public ModelElement Model
        {
            get { return ((ModelElement)(base["model"])); }
            set { base["model"] = value; }
        }

        [ConfigurationProperty("logic")]
        public LogicElement Logic
        {
            get { return ((LogicElement)(base["logic"])); }
            set { base["logic"] = value; }
        }

        [ConfigurationProperty("metadata")]
        public MetadataElement Metadata
        {
            get { return ((MetadataElement)(base["metadata"])); }
            set { base["metadata"] = value; }
        }

        [ConfigurationProperty("ef")]
        public EfElement Ef
        {
            get { return ((EfElement)(base["ef"])); }
            set { base["ef"] = value; }
        }

        [ConfigurationProperty("nh")]
        public NhElement Nh
        {
            get { return ((NhElement)(base["nh"])); }
            set { base["nh"] = value; }
        }


    }

}
