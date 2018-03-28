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

        public ApplicationElement this[string key] => (ApplicationElement)BaseGet(key);

        protected override ConfigurationElement CreateNewElement()
        {
            return new ApplicationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ApplicationElement)element).Name;
        }
    }
}
