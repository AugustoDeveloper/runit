using CommandLine;

namespace RunIt.Models
{
    public class RunAsOptions
    {
        [Option('d', "domain", Required = true, HelpText = "The domain name")]
        public string DomainName { get; set; }

        [Option('a', "app", Required = true, HelpText = "The application name what will execute on specific domain")]
        public string ApplicationAlias { get; set; }

        [Option('v', "verbose", Required = false, HelpText = "Show all logs executed by application")]
        public bool IsVerbose { get; set; }

    }
}