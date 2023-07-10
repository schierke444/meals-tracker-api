using Auth.Commons.Dtos;
using BuildingBlocks.Commons.CQRS;

namespace Auth.Features.Commands.RegisterUser.v1;

public record RegisterUserCommand(
    string Username, 
    string Password, 
    string Email,
    string? FirstName,
    string? LastName 
    ) : ICommand<(AuthDetailsDto, string)>;