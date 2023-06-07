using BuildingBlocks.Commons.CQRS;
using Meals.Commons.Dtos;

namespace Meals.Features.Ingredients.Queries.GetIngredients;

public record GetIngredientsQuery : IQuery<IEnumerable<IngredientsDto>>;