using XTrimCalculator.Domain.Interfaces;
using XTrimCalculator.Domain.Extensions;
using System.Collections.Generic;
using FluentValidation;
using XTrimCalculator.Domain.Entities;
using System.Linq;

namespace XTrimCalculator.Application
{
    public class CalculatorOrchestrator : ICalculatorOrchestrator
    {
        private readonly IValidator<IEnumerable<Instruction>> instructionsValidator;
        private readonly ICalculatorService calculatorService;

        public CalculatorOrchestrator(IValidator<IEnumerable<Instruction>> instructionsValidator, ICalculatorService calculatorService)
        {
            this.instructionsValidator = instructionsValidator;
            this.calculatorService = calculatorService;
        }

        public OrchestratorResponse<decimal> Execute(IEnumerable<string> lines)
        {
            var response = new OrchestratorResponse<decimal>();

            var instructions = lines.CreateInstructions();

            if (instructions.Any() == false) return response;

            var validationResult = instructionsValidator.Validate(instructions);
            if (validationResult.IsValid)
                response.Result = calculatorService.Calculate(instructions);
            else
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage);

            return response;
        }
    }
}