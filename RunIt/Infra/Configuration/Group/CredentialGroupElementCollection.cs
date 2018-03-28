using System;
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

        public new CredentialElement this[string key]
        {
            get
            {
                if (BaseGet(key) == null)
                {
                    throw new NullReferenceException($"O nome {key} não existe dentro dos nós de credenciais.");
                }
                return (CredentialElement)BaseGet(key);
            }
        }

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
