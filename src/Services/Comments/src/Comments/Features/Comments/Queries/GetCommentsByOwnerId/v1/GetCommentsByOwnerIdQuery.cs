using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Comments.Features.Comments.Dtos;

namespace Comments.Features.Comments.Queries.GetCommentsByOwnerId.v1;

public sealed record GetCommentsByOwnerIdQuery (
    string OwnerId,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize    
): IQuery<PaginatedResults<CommentDetailsDto>>;