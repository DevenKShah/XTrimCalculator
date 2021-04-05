using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Collections.Generic;
using System.Linq;
using XTrimCalculator.Application;
using XTrimCalculator.Domain.Entities;
using Xunit;
using AutoFixture.Xunit2;

namespace XTrimCalculator.UnitTests.Application.CalculatorOrchestratorTests
{
    public class GivenInvalidInstructions
    {
        [Theory, AutoMoqData]
        public void When_Empty_Lines_Then_Return_Zero(CalculatorOrchestrator sut)
        {
            //Arrange

            //Act
            var result = sut.Execute(new[] { " ", " " });

            //Assert
            result.Result.Should().Be(0);
        }

        [Theory, AutoMoqData]
        public void When_Invalid_Lines_Then_Return_Error([Frozen] Mock<IValidator<IEnumerable<Instruction>>> validatorMock, CalculatorOrchestrator sut)
        {
            //Arrange
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

        [Theory, AutoMoqData]
        public void When_Bad_Data_Then_Return_Error(CalculatorOrchestrator sut)
        {
            //Arrange

            //Act
            var result = sut.Execute(new[] { "ad 1", "subtract 2" });

            //Assert
            result.Result.Should().Be(0);
            result.HasErrors.Should().BeTrue();
            result.Errors.Should().HaveCount(1);
            result.Errors.First().Should().StartWith("Exception on line 1");
        }

    }
}
