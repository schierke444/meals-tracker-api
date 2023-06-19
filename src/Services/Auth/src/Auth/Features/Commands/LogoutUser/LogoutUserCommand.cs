using BuildingBlocks.Commons.CQRS;
using MediatR;

namespace Auth.Features.Commands.LogoutUser;

public record LogoutUserCommand : ICommand<Unit>;