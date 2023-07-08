using BuildingBlocks.Commons.CQRS;
using MediatR;

namespace Auth.Features.Commands.LogoutUser.v1;

public record LogoutUserCommand : ICommand<Unit>;