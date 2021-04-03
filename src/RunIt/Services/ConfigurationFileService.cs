using System.Linq;
using RunIt.Configurations;

namespace RunIt.Services
{
    public class ConfigurationFileService : IConfigurationService
    {
        private readonly Domain[] domains;
        private readonly Application[] applications;

        public ConfigurationFileService(Domain[] domains, Application[] applications)
        {
            this.domains = domains;
            this.applications = applications;
        }

        Application IConfigurationService.GetApplication(string applicationNameOrAlias)
            => applications.FirstOrDefault(a => string.Equals(applicationNameOrAlias, a.Name, System.StringComparison.InvariantCultureIgnoreCase) || string.Equals(applicationNameOrAlias, a.Alias, System.StringComparison.InvariantCultureIgnoreCase));

        Domain IConfigurationService.GetDomain(string domainName)
            => domains.FirstOrDefault(a => string.Equals(domainName, a.Alias, System.StringComparison.InvariantCultureIgnoreCase));
    }
}