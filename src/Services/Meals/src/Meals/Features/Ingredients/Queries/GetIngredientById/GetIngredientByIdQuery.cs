using BuildingBlocks.Commons.CQRS;
using Meals.Commons.Dtos;

namespace Meals.Features.Ingredients.Queries.GetIngredientById;

public record GetIngredientByIdQuery(string IngredientId) : IQuery<IngredientDetailsDto>
{
    
}