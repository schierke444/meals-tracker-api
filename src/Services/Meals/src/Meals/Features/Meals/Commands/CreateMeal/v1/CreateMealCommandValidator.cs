using FluentValidation;

namespace Meals.Features.Meals.Commands.CreateMeal.v1;

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

        RuleFor(x => x.Instructions)
            .MinimumLength(4)
            .MaximumLength(200)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Rating)
            .LessThanOrEqualTo(10)
            .GreaterThanOrEqualTo(1)
            .NotEmpty()
            .NotNull();
        
        RuleForEach(x => x.Ingredients)
            .SetValidator(new AddIngredientsToMealValidator());

        RuleForEach(x => x.Categories)
            .SetValidator(new AddCategoryToMealValidator());
    } 
}