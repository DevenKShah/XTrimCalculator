using Xunit;
using XTrimCalculator.Domain.Extensions;
using XTrimCalculator.Domain.Entities;
using FluentAssertions;
using System;

namespace XTrimCalculator.UnitTests.Domain.Extensions
{
    public class WhenApplyingInstructions
    {
        [Theory]
        [InlineData(Operation.Add, 10, 10, 20)]
        [InlineData(Operation.Add, -10, 10, 0)]
        [InlineData(Operation.Subtract, 10, 1, 9)]
        [InlineData(Operation.Subtract, -10, 1, -11)]
        [InlineData(Operation.Multiply, 10, 2, 20)]
        [InlineData(Operation.Multiply, 0, 2, 0)]
        [InlineData(Operation.Multiply, 10, 0, 0)]
        [InlineData(Operation.Divide, 10, 2, 5)]
        [InlineData(Operation.Divide, 5, 2, 2.5)]
        [InlineData(Operation.Divide, 0, 2, 0)]
        public void Then_Return_Correct_Result(Operation operation, decimal startValue, decimal instructionValue, decimal expectedResult)
        {
            //Arrange
            var instruction = new Instruction(operation, instructionValue);
            //Act
            var result = startValue.ApplyInstruction(instruction);
            //Assert 
            result.Should().Be(expectedResult);
        }


        [Fact]
        public void When_Operation_is_Divide_And_Value_Is_Zero_Then_Error()
        {
            //Arrange
            var startValue = 10m;
            var instruction = new Instruction(Operation.Divide, 0);
            //Act
            Action act = () => startValue.ApplyInstruction(instruction);
            //Assert 
            act.Should().Throw<DivideByZeroException>();
        }
    }
}
