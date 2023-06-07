using FluentValidation;

namespace Category.Features.Commands.CreateCategory;

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