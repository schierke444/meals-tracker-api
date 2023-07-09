using FluentValidation;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Commands.CreateMeal.v1;

public class AddIngredientsToMealValidator : AbstractValidator<AddIngredientsToMealsDto>
{
    public AddIngredientsToMealValidator()
    {
        RuleFor(x => x.Id) 
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Amount) 
            .GreaterThanOrEqualTo(1);
    } 
}