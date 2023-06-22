using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Category.Features.Dtos;

namespace Category.Features.Queries.GetCategories;

public sealed record GetCategoriesQuery(
    string? Search,
    string? SortColumn,
    string? SortOrder,
    int Page = 1,
    int PageSize = 10
) : IQuery<PaginatedResults<CategoryDetailsDto>>;