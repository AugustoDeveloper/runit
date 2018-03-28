using System.Configuration;

namespace RunIt.Infra.Configuration.Element
{
    public class CredentialElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true)]
        public string Name => this["name"].ToString();

        [ConfigurationProperty("username", IsRequired = true)]
        public string Username => this["username"].ToString();

        [ConfigurationProperty("domain", IsRequired = true)]
        public string Domain => this["domain"].ToString();

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password => this["password"].ToString();
    }
}
