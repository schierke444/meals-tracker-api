using FluentValidation;

namespace Auth.Features.Commands.RegisterUser.v1;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
         RuleFor(x => x.Username)
            .MinimumLength(4)
            .MaximumLength(12)
            .NotNull()
            .NotEmpty();


        RuleFor(x => x.Password)
            .MinimumLength(4)
            .MaximumLength(12)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull();
    }
}