using System.Collections.Generic;

namespace XTrimCalculator.Application
{
    public interface ICalculatorOrchestrator
    {
        OrchestratorResponse<decimal> Execute(IEnumerable<string> lines);
    }
}
