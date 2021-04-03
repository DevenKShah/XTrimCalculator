using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using XTrimCalculator.Domain.Interfaces;
using XTrimCalculator.Infrastructure.Services;
using XTrimCalculator.Application;
using FluentValidation;
using System.Collections.Generic;
using XTrimCalculator.Domain.Entities;
using System;

namespace XTrimCalculator.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            var logger = host.Services.GetService<ILogger<Program>>();
            Console.WriteLine(Environment.NewLine);

            if (args.Length == 0)
            {
                logger.LogError("A valid file path is required");
                return;
            }

            var calculator = host.Services.GetService<Calculator>();

            if (args.Length == 1)
            {
                logger.LogInformation($"Reading from file");
                await calculator.Calculate(args[0]);
            }
            else
            {
                logger.LogInformation("Reading from arguments");
                calculator.Calculate(args);
            }

           // await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) => 
            Host
                .CreateDefaultBuilder(args)
                .ConfigureLogging(l => 
                {
                    //l.ClearProviders();
                    l.AddSimpleConsole(options => 
                    {
                        options.IncludeScopes = true;
                        options.SingleLine = true;
                        options.TimestampFormat = "hh:mm:ss ";
                    });
                })
                .ConfigureServices((_, services) => 
                {
                    services.AddTransient<IFileReadService, FileReadService>();
                    services.AddTransient<IValidator<IEnumerable<Instruction>>, InstructionsValidator>();
                    services.AddTransient<ICalculatorService, CalculatorService>();
                    services.AddTransient<ICalculatorOrchestrator, CalculatorOrchestrator>();
                    services.AddTransient<Calculator>();
                });
    }
}
