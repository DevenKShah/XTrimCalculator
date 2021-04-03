using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Collections.Generic;
using XTrimCalculator.Application;
using XTrimCalculator.Domain.Entities;
using XTrimCalculator.Domain.Interfaces;
using Xunit;

namespace XTrimCalculator.UnitTests.Application.CalculatorOrchestratorTests
{
    public class GivenValidInstructions
    {
        CalculatorOrchestrator sut;
        Mock<IValidator<IEnumerable<Instruction>>> validatorMock = new Mock<IValidator<IEnumerable<Instruction>>>();
        Mock<ICalculatorService> calulatorServiceMock = new Mock<ICalculatorService>();
        const decimal expectedResult = 10;
        OrchestratorResponse<decimal> result;

        public GivenValidInstructions()
        {
            //Arrange
            validatorMock.Setup(v => v.Validate(It.IsAny<IEnumerable<Instruction>>())).Returns(new ValidationResult());
            calulatorServiceMock.Setup(c => c.Calculate(It.IsAny<IEnumerable<Instruction>>())).Returns(expectedResult);
            sut = new CalculatorOrchestrator(validatorMock.Object, calulatorServiceMock.Object);

            //Act
            result = sut.Execute(new[] {"add 1", "apply 5"});
        }

        [Fact]
        public void Then_Call_Validator()
        {            
            //Assert
            validatorMock.Verify(v => v.Validate(It.IsAny<IEnumerable<Instruction>>()));
        }

        [Fact]
        public void Then_Call_Calculate()
        {
            //Assert
            calulatorServiceMock.Verify(c => c.Calculate(It.IsAny<IEnumerable<Instruction>>()));
        }

        [Fact]
        public void Then_Return_Calculated_Result()
        {
            result.Result.Should().Be(expectedResult);
        }
    }
}
