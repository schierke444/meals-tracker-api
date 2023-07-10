using FluentValidation;
using Meals.Features.Meals.Commands.CreateMeal.v1;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Commands.UpdateMeal.v1;

public class UpdateMealValidator : AbstractValidator<UpdateMealDto>
{
   public UpdateMealValidator()
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