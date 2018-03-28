using System.Configuration;

namespace RunIt.Infra.Configuration.Element
{
    public class ApplicationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name { get; set; }
        [ConfigurationProperty("filename", IsRequired = true)]
        public string Filename { get; set; }
        [ConfigurationProperty("alisa", IsKey = true)]
        public string Alias { get; set; }
    }
}