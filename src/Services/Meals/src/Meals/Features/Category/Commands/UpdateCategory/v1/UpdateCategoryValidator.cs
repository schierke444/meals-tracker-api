using Category.Features.Dtos;
using FluentValidation;

namespace Meals.Features.Category.Commands.UpdateCategory.v1;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator()
    {
          RuleFor(x => x.Name) 
            .MinimumLength(4)
            .MaximumLength(20)
            .NotNull()
            .NotEmpty();
    } 
}