using System;
using System.Configuration;
using RunIt.Infra.Configuration.Element;


namespace RunIt.Infra.Configuration.Group
{
    public class ApplicationGroupElementCollection : ConfigurationElementCollection
    {
        public ApplicationElement this[int index]
        {
            get { return (ApplicationElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        public new ApplicationElement this[string key]
        {
            get
            {
                if (BaseGet(key) == null)
                {
                    throw new NullReferenceException($"O alias {key} não existe dentro dos nós de aplicações.");
                }
                return (ApplicationElement) BaseGet(key);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ApplicationElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new ApplicationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ApplicationElement)element).Alias;
        }
    }
}
