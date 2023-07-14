using FluentValidation;

namespace Comments.Features.Comments.Commands.AddComments.v1;

public sealed class AddCommentsCommandValidator : AbstractValidator<AddCommentsCommand>
{
    public AddCommentsCommandValidator()
    {
        RuleFor(x => x.PostId) 
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Content)
            .MinimumLength(8)
            .MaximumLength(200)
            .NotEmpty()
            .NotNull();
    } 
}