using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace XTrimCalculator.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            var logger = host.Services.GetService<ILogger<Program>>();
            logger.LogInformation("Hello World!");

            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) => 
            Host
                .CreateDefaultBuilder(args)
                .ConfigureLogging(l => 
                {
                    l.ClearProviders();
                    l.AddConsole();
                });
    }
}
