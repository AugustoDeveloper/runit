using System.Configuration;

namespace RunIt.Infra.Configuration.Element
{
    public class ApplicationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name => this["name"].ToString();

        [ConfigurationProperty("filename", IsRequired = true)]
        public string Filename => this["filename"].ToString();

        [ConfigurationProperty("alias", IsKey = true)]
        public string Alias => this["alias"].ToString();
    }
}