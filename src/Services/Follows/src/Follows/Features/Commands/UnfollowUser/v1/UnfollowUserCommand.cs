using MediatR;

namespace Follows.Features.Commands.UnfollowUser.v1;

public record UnfollowUserCommand(string UserToFollowId) : IRequest;

