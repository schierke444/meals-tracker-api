using BuildingBlocks.Commons.CQRS;
using MediatR;

namespace Comments.Features.Likes.Commands.UnlikeComment.v1;

public sealed record UnlikeCommentCommand(string CommentId) : ICommand<Unit>;