using System;
using System.Configuration;
using RunIt.Infra.Configuration.Group;

namespace RunIt.Infra.Configuration
{
    public class EnviromentConfigurationSection : ConfigurationSection
    {
        public const string EnviromentSectionName = "enviroment";

        [ConfigurationProperty("credentials")]
        public CredentialGroupElementCollection Credentials => (CredentialGroupElementCollection)this["credentials"];

        [ConfigurationProperty("applications")]
        public ApplicationGroupElementCollection Applications => (ApplicationGroupElementCollection)this["applications"];

        public static EnviromentConfigurationSection Get()
        {
            return ConfigurationManager.GetSection(EnviromentSectionName) as EnviromentConfigurationSection;
        }


        public void Set(string elementName, object value)
        {
            this[elementName] = value;
        }
    }
}
