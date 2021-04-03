using RunIt.Configurations;

namespace RunIt.Services
{
    public interface IConfigurationService
    {
        Domain GetDomain(string domainName);
        Application GetApplication(string applicationNameOrAlias);
    }
}