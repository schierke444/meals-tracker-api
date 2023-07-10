using FluentValidation;
using Meals.Features.Ingredients.Dtos;

namespace Meals.Features.Ingredients.Commands.UpdateIngredient.v1;

public class UpdateIngredientValidator : AbstractValidator<UpdateIngredientDto>
{
    public UpdateIngredientValidator()
    {
        RuleFor(x => x.Name) 
            .MinimumLength(4)
            .MaximumLength(12)
            .NotEmpty()
            .NotNull(); 
    } 
}