using BuildingBlocks.Commons.CQRS;
using MediatR;

namespace Comments.Features.Comments.Commands.DeleteComments.v1;

public sealed record DeleteCommentCommand(string CommentId) : ICommand<Unit>;