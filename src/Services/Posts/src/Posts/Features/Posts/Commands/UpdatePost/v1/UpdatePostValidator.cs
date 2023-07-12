using FluentValidation;
using Posts.Features.Posts.Dtos;

namespace Posts.Features.Posts.Commands.UpdatePost.v1;

public class UpdatePostValidator : AbstractValidator<UpdatePostDto>
{
    public UpdatePostValidator()
    {
        RuleFor(x => x.Content) 
            .MinimumLength(4)
            .MaximumLength(150)
            .NotNull()
            .NotEmpty();
    }
}