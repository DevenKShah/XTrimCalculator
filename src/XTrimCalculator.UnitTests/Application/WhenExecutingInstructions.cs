using FluentAssertions;
using System;
using System.Collections.Generic;
using XTrimCalculator.Application;
using XTrimCalculator.Domain.Entities;
using Xunit;

namespace XTrimCalculator.UnitTests.Application
{
    public class WhenExecutingInstructions
    {
        [Theory]
        [InlineData("a:5, s:2, m:3, d:4, x:5", 6)]
        [InlineData("a:5, a:2, a:3, d:3, x:5", 5)]
        [InlineData("m:2, d:5, m:3, d:3, x:5", 2)]
        [InlineData("d:2, s:2, m:3, a:4, x:5", 5.5)]
        public void Then_Apply_Instructions_In_Correct_Order(string data, decimal expectedResult)
        {
            //Arrange
            var instructions = GetInstructions(data);
            var calculatorService = new CalculatorService();
            //Act
            var result = calculatorService.Calculate(instructions);
            //Assert
            result.Should().Be(expectedResult);
        }

        private IEnumerable<Instruction> GetInstructions(string theoryData)
        {
            var splitData = theoryData.Split(',');
            foreach(var data in splitData)
            {
                var values = data.Split(':');
                yield return values[0].Trim() switch
                {
                    "a" => new Instruction("add", values[1]),
                    "s" => new Instruction("subtract", values[1]),
                    "m" => new Instruction("multiply", values[1]),
                    "d" => new Instruction("divide", values[1]),
                    _ => new Instruction("apply", values[1]),
                };
            }
        }
    }
}
