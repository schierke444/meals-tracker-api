using Auth.Commons.Dtos;
using BuildingBlocks.Commons.CQRS;

namespace Auth.Features.Commands.RegisterUser;
public sealed record RegisterUserCommand(string Username, string Password, string Email) : ICommand<(AuthDetailsDto, string)>
{
}
