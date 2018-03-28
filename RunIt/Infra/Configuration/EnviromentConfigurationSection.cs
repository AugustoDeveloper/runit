using System.Configuration;
using RunIt.Infra.Configuration.Group;

namespace RunIt.Infra.Configuration
{
    public class EnviromentConfigurationSection : ConfigurationSection
    {
        public const string EnviromentSectionName = "enviroment";

        [ConfigurationProperty("credentials")]
        public CredentialGroupElementCollection Credentials { get; set; }
        [ConfigurationProperty("applications")]
        public ApplicationGroupElementCollection Applications { get; set; }

        public static EnviromentConfigurationSection Get()
        {
            return ConfigurationManager.GetSection(EnviromentSectionName) as EnviromentConfigurationSection;
        }
    }
}
