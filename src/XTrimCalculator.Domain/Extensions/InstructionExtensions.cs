using System;
using System.Collections.Generic;
using System.Linq;
using XTrimCalculator.Domain.Entities;

namespace XTrimCalculator.Domain.Extensions
{
    public static class InstructionExtensions
    {
        public static List<Instruction> CreateInstructions(this IEnumerable<string> instructionLines)
        {
            return instructionLines.Where(l => string.IsNullOrWhiteSpace(l) == false).Select((line, index) =>
            {
                var splitData = line.Split(" ");
                if (splitData.Length != 2)
                {
                    throw new ArgumentException($"Invalid line {index + 1}");
                }
                try
                {
                    return new Instruction(splitData[0], splitData[1]);
                }
                catch(ArgumentException ex)
                {
                    throw new ArgumentException($"Exception on line {index + 1}", ex);
                }
            }).ToList();
        }
    }
}