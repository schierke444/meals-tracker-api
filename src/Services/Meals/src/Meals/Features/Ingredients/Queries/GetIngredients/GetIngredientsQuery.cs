using BuildingBlocks.Commons.CQRS;
using Meals.Features.Ingredients.Dtos;

namespace Meals.Features.Ingredients.Queries.GetIngredients;

public record GetIngredientsQuery : IQuery<IEnumerable<IngredientsDto>>;