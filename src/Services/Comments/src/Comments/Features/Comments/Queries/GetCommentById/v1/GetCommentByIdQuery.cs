using BuildingBlocks.Commons.CQRS;
using Comments.Features.Comments.Dtos;

namespace Comments.Features.Comments.Queries.GetCommentById.v1;

public sealed record GetCommentByIdQuery(string CommentId) : IQuery<CommentDetailsDto>;