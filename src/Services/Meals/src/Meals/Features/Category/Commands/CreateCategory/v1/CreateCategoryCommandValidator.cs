using FluentValidation;

namespace Meals.Features.Category.Commands.CreateCategory.v1;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name) 
            .MinimumLength(4)
            .MaximumLength(20)
            .NotNull()
            .NotEmpty();
    } 
}