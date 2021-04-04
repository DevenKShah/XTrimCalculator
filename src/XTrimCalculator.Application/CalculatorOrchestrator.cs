using XTrimCalculator.Domain.Interfaces;
using XTrimCalculator.Domain.Extensions;
using System.Collections.Generic;
using FluentValidation;
using XTrimCalculator.Domain.Entities;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;

namespace XTrimCalculator.Application
{
    public class CalculatorOrchestrator : ICalculatorOrchestrator
    {
        private readonly IValidator<IEnumerable<Instruction>> instructionsValidator;
        private readonly ICalculatorService calculatorService;
        private readonly ILogger<CalculatorOrchestrator> logger;

        public CalculatorOrchestrator(IValidator<IEnumerable<Instruction>> instructionsValidator, ICalculatorService calculatorService, ILogger<CalculatorOrchestrator> logger)
        {
            this.instructionsValidator = instructionsValidator;
            this.calculatorService = calculatorService;
            this.logger = logger;
        }

        public OrchestratorResponse<decimal> Execute(IEnumerable<string> lines)
        {
            var response = new OrchestratorResponse<decimal>();

            try
            {
                var instructions = lines.CreateInstructions();
                
                if (instructions.Any() == false) return response;

                logger.LogInformation($"Found {instructions.Count} instructions");

                var validationResult = instructionsValidator.Validate(instructions);
                if (validationResult.IsValid)
                    response.Result = calculatorService.Calculate(instructions);
                else
                    response.Errors = validationResult.Errors.Select(e => e.ErrorMessage);
            }
            catch (ArgumentException ex)
            {
                response.Errors = new[] { $"{ex.Message}: {ex.InnerException.Message}" };
            }
            catch (OverflowException ex)
            {
                response.Errors = new[] { "The sum of numbers is too long. Brain fart." };
            }
            return response;
        }
    }
}