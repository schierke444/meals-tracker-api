using Comments.Features.Comments.Dtos;
using FluentValidation;

namespace Comments.Features.Comments.Commands.UpdateComments.v1;

public sealed class UpdateCommentValidator : AbstractValidator<UpdateCommentDto>
{
    public UpdateCommentValidator()
    {
        RuleFor(x => x.Content)
            .MinimumLength(8)
            .MaximumLength(200)
            .NotEmpty()
            .NotNull();
    } 
}