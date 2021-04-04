using System.Collections.Generic;
using System.Linq;
using XTrimCalculator.Domain.Entities;
using XTrimCalculator.Domain.Extensions;
using XTrimCalculator.Domain.Interfaces;

namespace XTrimCalculator.Application
{
    public class CalculatorService : ICalculatorService
    {
        public decimal Calculate(IEnumerable<Instruction> instructions)
        {
            var applyInstruction = instructions.First(i => i.Operation == Operation.Apply);
            var otherInstructions = instructions.Where(i => i.Operation != Operation.Apply);

            return otherInstructions.Aggregate(applyInstruction.Value, (sum, val) => sum.ApplyInstruction(val));
        }
    }
}