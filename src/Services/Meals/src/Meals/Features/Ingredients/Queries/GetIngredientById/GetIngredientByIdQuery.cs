using BuildingBlocks.Commons.CQRS;
using Meals.Features.Ingredients.Dtos;

namespace Meals.Features.Ingredients.Queries.GetIngredientById;

public record GetIngredientByIdQuery(string IngredientId) : IQuery<IngredientDetailsDto>
{
    
}