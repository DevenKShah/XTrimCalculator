using Microsoft.Extensions.Logging;
using XTrimCalculator.Application;
using System.Linq;
using XTrimCalculator.Domain.Interfaces;
using System.IO;
using System;
using System.Threading.Tasks;

namespace XTrimCalculator.ConsoleApp
{
    public class Calculator
    {
        private readonly ICalculatorOrchestrator orchestrator;
        private readonly ILogger<Calculator> logger;
        private readonly IFileReadService fileReadService;

        public Calculator(ICalculatorOrchestrator orchestrator, ILogger<Calculator> logger, IFileReadService fileReadService)
        {
            this.orchestrator = orchestrator;
            this.logger = logger;
            this.fileReadService = fileReadService;
        }

        public async Task Calculate(string filePath)
        {
            try
            {
                var lines = await fileReadService.ReadAllLines(filePath);
                Calculate(lines);
            }
            catch (FileNotFoundException)
            {
                logger.LogError($"File not found at path {filePath}");
            }
            catch (Exception)
            {
                logger.LogError($"Error reading file at path {filePath}");
            }
        }

        public void Calculate(string[] lines)
        {
            logger.LogWarning($"Processing instructions: {string.Join(',', lines)}");
            var response = orchestrator.Execute(lines);
            if (response.HasErrors)
            {
                response.Errors.ToList().ForEach(e => logger.LogError(e));
            }
            else
            {
                logger.LogWarning($"Result: {response.Result}");
                Console.WriteLine(Environment.NewLine + Environment.NewLine + response.Result);
            }
        }
    }
}
