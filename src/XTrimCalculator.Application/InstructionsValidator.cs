using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using XTrimCalculator.Domain.Entities;

namespace XTrimCalculator.Application
{
    public class InstructionsValidator : AbstractValidator<IEnumerable<Instruction>>
    {
        public static class ErrorMessages
        {
            public const string NotEnoughInstructions = "At least two valid instructions are required";
            public const string LastOperationIsNotApply = "The last instruction should be Apply";
            public const string InvalidOperation = "Expected Add, Subtract, Multiply, Divide or Apply";
            public const string DivisionByZero = "Operation Divide cannot have value 0";
            public const string ThereCanBeOnlyOneApplyOperation = "There can be only one Apply instruction";
        }
        public InstructionsValidator()
        {
            RuleFor(x => x.Count()).GreaterThan(1).WithMessage(ErrorMessages.NotEnoughInstructions);
            RuleFor(x => x.Count(x => x.Operation == Operation.Apply)).Equal(0).WithMessage(ErrorMessages.ThereCanBeOnlyOneApplyOperation);
            When(x => x.Count() > 1, () =>
            {
                RuleFor(x => x.Last().Operation).Equal(Operation.Apply).WithMessage(ErrorMessages.LastOperationIsNotApply);
                RuleForEach(x => x).ChildRules(x =>
                {
                    x.RuleFor(x => x.Operation).IsInEnum().WithMessage(ErrorMessages.InvalidOperation);
                    x.RuleFor(x => x.Value).GreaterThan(0).When(x => x.Operation == Operation.Divide).WithMessage(ErrorMessages.DivisionByZero);
                });
            });
        }
    }
}
