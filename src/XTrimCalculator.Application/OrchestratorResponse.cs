using System.Collections.Generic;
using System.Linq;

namespace XTrimCalculator.Application
{
    public class OrchestratorResponse<T>
    {
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
        public bool HasErrors => Errors.Any();

        public T Result { get; set; }
    }
}