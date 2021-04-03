using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RunIt.Configurations;
using RunIt.Infra.Utils;
using RunIt.Models;
using RunIt.Services;

namespace RunIt
{
    public class Program
    {
        static private IConfigurationService Service;

        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appSettings.json", false)
                .AddJsonFile("appSettings.Production.json", false)
                .Build();


            var localEnvironment = configuration.GetSection("Env").Get<LocalEnvironment>();

            var domains = localEnvironment?.Domains ?? Array.Empty<Domain>();
            var applications = localEnvironment?.Applications ?? Array.Empty<Application>();

            Service = new ConfigurationFileService(
                domains, 
                applications);

            CommandLine
                .Parser
                .Default
                .ParseArguments<RunAsOptions>(args)
                .WithParsed(RunAsWithOptions);
        }

        static private void RunInternalProcess(Domain domain, Application application, ILogger logger)
        {
            WinApi.RunAs(domain.Name, domain.Username, domain.Password, application.Filename, logger);
        }

        static private void RunAsWithOptions(RunAsOptions options)
        {
            using var factory = LoggerFactory
                .Create(_ => _.ClearProviders()
                    .SetMinimumLevel(options.IsVerbose ? LogLevel.Trace : LogLevel.Information)
                    .AddConsole());

            var logger = factory.CreateLogger<Program>();

            try
            {   
                var domain = Service.GetDomain(options.DomainName);
                domain = domain ?? throw new InvalidOperationException("Domain not found");

                logger.LogTrace($"Domain {domain.Password}...");

                var application = Service.GetApplication(options.ApplicationAlias);
                application = application ?? throw new InvalidOperationException("Application not found");

                logger.LogTrace($"Aplication {application.Name}...");

                if (!File.Exists(application.Filename))
                {
                    throw new InvalidOperationException("The executable file was not found");
                }

                logger.LogTrace($"Authenticating the {application.Name} with {domain.Name}\\{domain.Username}... \nFile: {application.Filename}...");

                RunInternalProcess(domain, application, logger);
                
            }
            catch(InvalidOperationException ex)
            {
                logger.LogError(ex.Message);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
            }            
        }
    }
}