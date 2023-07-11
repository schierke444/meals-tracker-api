using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Posts.Features.Posts.Dtos;

namespace Posts.Features.Posts.Queries.GetPosts.v1;

public record GetPostsQuery (
    string? Search,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize
) : IQuery<PaginatedResults<PostDetailsDto>>;