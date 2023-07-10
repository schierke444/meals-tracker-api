using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Meals.Features.Ingredients.Dtos;

namespace Meals.Features.Ingredients.Queries.GetIngredients;

public record GetIngredientsQuery(
    string? Search,
    string? SortColumn,
    string? SortOrder,
    int Page = 1,
    int PageSize = 10
) : IQuery<PaginatedResults<IngredientsDto>>;