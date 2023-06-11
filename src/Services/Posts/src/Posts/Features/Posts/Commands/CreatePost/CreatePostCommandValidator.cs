﻿using FluentValidation;

namespace Posts.Features.Posts.Commands.CreatePost;

public sealed class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Content) 
            .MinimumLength(4)
            .MaximumLength(150)
            .NotNull()
            .NotEmpty();
    }
}
