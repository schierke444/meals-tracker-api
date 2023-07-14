using BuildingBlocks.Commons.CQRS;
using MediatR;

namespace Comments.Features.Likes.Commands.LikeComment.v1;

public sealed record LikeCommentCommand(string CommentId) : ICommand<Unit>;