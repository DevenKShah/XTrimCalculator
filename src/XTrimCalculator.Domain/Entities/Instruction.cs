using System;

namespace XTrimCalculator.Domain.Entities
{
    public class Instruction
    {
        public Operation Operation { get; }
        public decimal Value { get; }
        public Instruction(Operation operation, decimal value)
        {
            Operation = operation;
            Value = value;
        }

        public Instruction(string operation, string value)
        {
            if (Enum.TryParse<Operation>(operation, true, out var op))
                Operation = op;
            else
                throw new ArgumentException($"Invalid operation {operation}, expected add, subtract, multiple, divide or apply");

            if (decimal.TryParse(value, out var val))
                Value = val;
            else
                throw new ArgumentException($"Invalid value {value}, expected a number");
        }
    }
}