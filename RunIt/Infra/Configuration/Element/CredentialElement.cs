using System.Configuration;

namespace RunIt.Infra.Configuration.Element
{
    public class CredentialElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true)]
        public string Name { get; set; }
        [ConfigurationProperty("username", IsRequired = true)]
        public string Username { get; set; }
        [ConfigurationProperty("domain", IsRequired = true)]
        public string Domain { get; set; }
        [ConfigurationProperty("password", IsRequired = true)]
        public string Password { get; set; }
    }
}
