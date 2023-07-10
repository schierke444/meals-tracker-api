using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Follows.Features.Dtos;

namespace Follows.Features.Queries.GetUsersFollowers.v1;

public record GetUsersFollowersQuery(
    string UserId,
    string? Search,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize
) : IQuery<PaginatedResults<UserDto>>;