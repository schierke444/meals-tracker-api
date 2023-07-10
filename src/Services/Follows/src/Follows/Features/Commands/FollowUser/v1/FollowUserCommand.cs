using System.Windows.Input;
using BuildingBlocks.Commons.CQRS;
using MediatR;

namespace Follows.Features.Commands.FollowUser.v1;

public record FollowUserCommand(string UserToFollowId ) : ICommand<Unit>;

