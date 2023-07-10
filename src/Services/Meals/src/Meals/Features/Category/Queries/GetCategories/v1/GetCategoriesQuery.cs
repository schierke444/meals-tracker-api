using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Category.Features.Dtos;

namespace Meals.Features.Category.Queries.GetCategories.v1;

public sealed record GetCategoriesQuery(
    string? Search,
    string? SortColumn,
    string? SortOrder,
    int Page = 1,
    int PageSize = 10
) : IQuery<PaginatedResults<CategoryDto>>;