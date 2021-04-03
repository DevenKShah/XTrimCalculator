using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using XTrimCalculator.Application;
using XTrimCalculator.Domain.Entities;
using XTrimCalculator.Domain.Interfaces;
using Xunit;

namespace XTrimCalculator.UnitTests.Application.CalculatorOrchestratorTests
{
    public class GivenInvalidInstructions
    {
        [Fact]
        public void When_Empty_Lines_Then_Return_Zero()
        {
            //Arrange
            var validatorMock = new Mock<IValidator<IEnumerable<Instruction>>>();
            var calulatorServiceMock = new Mock<ICalculatorService>();
            var sut = new CalculatorOrchestrator(validatorMock.Object, calulatorServiceMock.Object);

            //Act
            var result = sut.Execute(new[] { " ", " " });

            //Assert
            result.Result.Should().Be(0);
        }

        [Fact]
        public void When_Invalid_Lines_Then_Return_Error()
        {
            //Arrange
            var validatorMock = new Mock<IValidator<IEnumerable<Instruction>>>();
            var calulatorServiceMock = new Mock<ICalculatorService>();
            var sut = new CalculatorOrchestrator(validatorMock.Object, calulatorServiceMock.Object);
            validatorMock
                .Setup(v => v.Validate(It.IsAny<IEnumerable<Instruction>>()))
                .Returns(new ValidationResult(new[] { new ValidationFailure("field","error message") } ));

            //Act
            var result = sut.Execute(new[] { "add 1", "subtract 2" });

            //Assert
            result.Result.Should().Be(0);
            result.HasErrors.Should().BeTrue();
            result.Errors.Should().OnlyContain(c => c == "error message");
        }

    }
}
