using FluentValidation;

namespace Meals.Features.Ingredients.Commands.CreateIngredient;

public class CreateIngredientCommandValidator : AbstractValidator<CreateIngredientCommand>
{
    public CreateIngredientCommandValidator()
    {
        RuleFor(x => x.Name) 
            .MinimumLength(4)
            .MaximumLength(12)
            .NotEmpty()
            .NotNull();
    } 
}