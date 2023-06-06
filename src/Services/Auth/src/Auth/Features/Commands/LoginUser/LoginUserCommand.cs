using Auth.Commons.Dtos;
using BuildingBlocks.Commons.CQRS;

namespace Auth.Features.Commands.LoginUser;
public sealed record LoginUserCommand(string Username, string Password) : ICommand<(AuthDetailsDto, string)> 
{
}
