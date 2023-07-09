using FluentValidation;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Commands.CreateMeal.v1;

public class AddCategoryToMealValidator : AbstractValidator<AddCategoryToMealsDto>
{
    public AddCategoryToMealValidator()
    {
        RuleFor(x => x.Id) 
            .NotEmpty()
            .NotNull();
    } 
}