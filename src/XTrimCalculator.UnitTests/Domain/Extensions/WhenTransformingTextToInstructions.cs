using System.Collections.Generic;
using Xunit;
using XTrimCalculator.Domain.Extensions;
using FluentAssertions;
using System.Linq;
using XTrimCalculator.Domain.Entities;
using System;

namespace XTrimCalculator.UnitTests.Domain.Extensions
{
    public class WhenTransformingTextToInstructions
    {
        [Fact]
        public void And_If_Empty_Then_Return_Empty_Instuctions()
        {
            //Arrange
            var lines = new List<string>();
            //Act
            var instructions = lines.CreateInstructions();
            //Assert
            instructions.Should().BeEmpty();
        }

        [Fact]
        public void And_If_Valid_Lines_Then_Return_Transformed_Instructions()
        {
            //Arrange
            var lines = new List<string>();
            lines.Add("add 1");
            lines.Add("apply 4");
            //Act
            var instructions = lines.CreateInstructions();
            //Assert
            instructions.Count().Should().Be(2);
            instructions.First().Operation.Should().Be(Operation.Add);
            instructions.First().Value.Should().Be(1);
            instructions.Last().Operation.Should().Be(Operation.Apply);
            instructions.Last().Value.Should().Be(4);
        }

        [Fact]
        public void Then_Ignore_Empty_Lines()
        {
            //Arrange
            var lines = new List<string>();
            lines.Add("add 1");
            lines.Add("  ");
            lines.Add("apply 4");
            //Act
            var instructions = lines.CreateInstructions();
            //Assert
            instructions.Count().Should().Be(2);
        }

        [Fact]
        public void Then_Ignore_Preceding_And_Leading_Spaces()
        {
            //Arrange
            var lines = new List<string>();
            lines.Add(" add 1 ");
            lines.Add(" apply 4 ");
            //Act
            var instructions = lines.CreateInstructions();
            //Assert
            instructions.Count().Should().Be(2);
        }

        [Fact]
        public void Then_Ignore_Multiple_Spaces()
        {
            //Arrange
            var lines = new List<string>();
            lines.Add(" add   1 ");
            lines.Add(" apply  4 ");
            //Act
            var instructions = lines.CreateInstructions();
            //Assert
            instructions.Count().Should().Be(2);
        }

        [Fact]
        public void And_If_Line_Is_Invalid_Format_Then_Throw_Argument_Exception()
        {
            //Arrange
            var lines = new List<string>();
            lines.Add("add 1");
            lines.Add("asdfdf");
            //Act
            Action act = () => lines.CreateInstructions();
            //Assert
            act.Should().Throw<ArgumentException>().WithMessage("Invalid line 2");
        }

        [Fact]
        public void And_If_Line_Data_Is_Invalid_Then_Throw_Argument_Exception()
        {
            //Arrange
            var lines = new List<string>();
            lines.Add("add 1");
            lines.Add("asdf 2");
            //Act
            Action act = () => lines.CreateInstructions();
            //Assert
            act.Should().Throw<ArgumentException>().WithMessage("Exception on line 2").WithInnerException<ArgumentException>();
        }
    }
}
