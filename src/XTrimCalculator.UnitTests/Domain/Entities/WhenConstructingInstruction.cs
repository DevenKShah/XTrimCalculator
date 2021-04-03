using Xunit;
using FluentAssertions;
using XTrimCalculator.Domain.Entities;
using System;

namespace XTrimCalculator.UnitTests.Domain.Entities
{
    public class WhenConstructingInstruction
    {
        [Fact]
        public void And_If_Valid_Then_Create_Instance()
        {
            //Arrange
            var op = "add";
            var val = "1";
            //Act
            var instruction = new Instruction(op, val);
            //Assert
            instruction.Operation.Should().Be(Operation.Add);
            instruction.Value.Should().Be(1);
        }

        [Fact]
        public void And_If_Invalid_Operation_Then_Throw_Argument_Exception()
        {
            //Arrange
            var op = "xxx";
            var val = "1";
            //Act
            Action act = () => new Instruction(op, val);
            //Assert
            act.Should().Throw<ArgumentException>().WithMessage($"Invalid operation '{op}'. Expected add, subtract, multiple, divide or apply");
        }

        [Fact]
        public void And_If_Invalid_Value_Then_Throw_Argument_Exception()
        {
            //Arrange
            var op = "apply";
            var val = "x";
            //Act
            Action act = () => new Instruction(op, val);
            //Assert
            act.Should().Throw<ArgumentException>().WithMessage($"Invalid value {val}, expected a number");
        }
    }
}