using FluentValidation;

namespace Meals.Features.Meals.Commands.CreateMeal;

public sealed class CreateMealCommandValidator : AbstractValidator<CreateMealCommand>
{
    public CreateMealCommandValidator()
    {
        RuleFor(x => x.MealName) 
            .MinimumLength(4)
            .MaximumLength(24)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.MealReview)
            .MinimumLength(4)
            .MaximumLength(150);

        RuleFor(x => x.Rating)
            .LessThanOrEqualTo(10)
            .GreaterThanOrEqualTo(1)
            .NotEmpty()
            .NotNull();
    } 
}