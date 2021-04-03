using System.Collections.Generic;
using XTrimCalculator.Domain.Entities;

namespace XTrimCalculator.Domain.Interfaces
{
    public interface ICalculatorService
    {
        decimal Calculate(IEnumerable<Instruction> instructions);
    }
}
