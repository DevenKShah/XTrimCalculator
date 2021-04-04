using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using XTrimCalculator.Domain.Entities;

namespace XTrimCalculator.Domain.Extensions
{
    public static class InstructionExtensions
    {
        public static List<Instruction> CreateInstructions(this IEnumerable<string> instructionLines)
        {
            Func<string, string> CleanUpWhiteSpaces = source =>
            {
                var regex = new Regex("[ ]{2,}");
                return regex.Replace(source, " ").Trim();
            };

            return instructionLines.Where(l => string.IsNullOrWhiteSpace(l) == false).Select((line, index) =>
            {
                var splitData = CleanUpWhiteSpaces(line).Split(" ");
                if (splitData.Length != 2)
                {
                    throw new ArgumentException($"Invalid line {index + 1}");
                }
                try
                {
                    return new Instruction(splitData[0], splitData[1]);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException($"Exception on line {index + 1}", ex);
                }
            }).ToList();
        }

        public static decimal ApplyInstruction(this decimal startValue, Instruction instruction)
        {
            try
            {
                return instruction.Operation switch
                {
                    Operation.Add => startValue + instruction.Value,
                    Operation.Subtract => startValue - instruction.Value,
                    Operation.Multiply => startValue * instruction.Value,
                    Operation.Divide => startValue / instruction.Value,
                    _ => throw new InvalidOperationException()
                };
            }
            catch (OverflowException ex)
            {
                throw ex;
            }
        }
    }
}