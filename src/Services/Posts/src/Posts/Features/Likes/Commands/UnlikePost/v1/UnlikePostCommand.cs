using BuildingBlocks.Commons.CQRS;
using MediatR;

namespace Posts.Features.Likes.Commands.UnlikePost.v1;

public sealed record UnlikePostCommand(string PostId) : ICommand<Unit>;