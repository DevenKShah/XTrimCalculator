using FluentAssertions;
using XTrimCalculator.Application;
using XTrimCalculator.Domain.Entities;
using Xunit;

namespace XTrimCalculator.UnitTests.Application
{
    public class WhenValidatingInstructions
    {
        [Fact]
        public void And_There_Are_More_Than_One_Instruction_And_Last_Has_Apply_Operation_Then_Valid() 
        {
            //Arrange
            var instructions = new[]
            {
                new Instruction(Operation.Add, 1),
                new Instruction(Operation.Apply, 4)
            };
            var validator = new InstructionsValidator();
            //Act
            var result = validator.Validate(instructions);
            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void And_There_Are_Not_Enough_Insturctions_Then_Invalid()
        {
            //Arrange
            var instructions = new[]
            {
                new Instruction(Operation.Add, 1),
            };
            var validator = new InstructionsValidator();
            //Act
            var result = validator.Validate(instructions);
            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorMessage.Should().Be(InstructionsValidator.ErrorMessages.NotEnoughInstructions);
        }

        [Fact]
        public void And_The_Last_Operation_Is_Not_Apply_Then_Invalid()
        {
            //Arrange
            var instructions = new[]
            {
                new Instruction(Operation.Apply, 1),
                new Instruction(Operation.Add, 1),
            };
            var validator = new InstructionsValidator();
            //Act
            var result = validator.Validate(instructions);
            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorMessage.Should().Be(InstructionsValidator.ErrorMessages.LastOperationIsNotApply);
        }

        [Fact]
        public void And_There_Is_Any_Invalid_Operation_Then_Invalid()
        {
            //Arrange
            var instructions = new[]
            {
                new Instruction((Operation)10, 1),
                new Instruction(Operation.Apply, 1),
            };
            var validator = new InstructionsValidator();
            //Act
            var result = validator.Validate(instructions);
            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorMessage.Should().Be(InstructionsValidator.ErrorMessages.InvalidOperation);
        }

        [Fact]
        public void And_Instruction_With_Divide_Operation_Is_Zero_Then_Invalid()
        {
            //Arrange
            var instructions = new[]
            {
                new Instruction(Operation.Divide, 0),
                new Instruction(Operation.Apply, 1),
            };
            var validator = new InstructionsValidator();
            //Act
            var result = validator.Validate(instructions);
            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorMessage.Should().Be(InstructionsValidator.ErrorMessages.DivisionByZero);
        }

        [Fact]
        public void And_More_Than_One_Apply_Operation_Then_Invalid()
        {
            //Arrange
            var instructions = new[]
            {
                new Instruction(Operation.Apply, 0),
                new Instruction(Operation.Apply, 1),
            };
            var validator = new InstructionsValidator();
            //Act
            var result = validator.Validate(instructions);
            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorMessage.Should().Be(InstructionsValidator.ErrorMessages.ThereCanBeOnlyOneApplyOperation);
        }
    }
}
