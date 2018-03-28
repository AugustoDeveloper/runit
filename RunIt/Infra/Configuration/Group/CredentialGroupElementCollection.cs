using System.Configuration;
using RunIt.Infra.Configuration.Element;

namespace RunIt.Infra.Configuration.Group
{
    public class CredentialGroupElementCollection : ConfigurationElementCollection 
    {
        public CredentialElement this[int index]
        {
            get { return (CredentialElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        public CredentialElement this[string key] => (CredentialElement)BaseGet(key);

		protected override ConfigurationElement CreateNewElement()
		{
            return new CredentialElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
            return ((CredentialElement)element).Name;
		}
	}
}
