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

        public static decimal ApplyInstruction(this decimal startValue, Instruction instruction) =>
            instruction.Operation switch
            {
                Operation.Add => startValue + instruction.Value,
                Operation.Subtract => startValue - instruction.Value,
                Operation.Multiply => startValue * instruction.Value,
                Operation.Divide => startValue / instruction.Value,
                _ => throw new InvalidOperationException()
            };
    }
}