using BuildingBlocks.Commons.CQRS;
using MediatR;

namespace Posts.Features.Likes.Commands.LikePost.v1;

public sealed record LikePostCommand(string PostId) : ICommand<Unit>;