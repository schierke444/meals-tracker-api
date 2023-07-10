using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Follows.Features.Dtos;

namespace Follows.Features.Queries.GetUsersFollowings.v1;
public record GetUsersFollowingsQuery(
    string UserId,
    string? Search,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize
) : IQuery<PaginatedResults<UserDto>>;