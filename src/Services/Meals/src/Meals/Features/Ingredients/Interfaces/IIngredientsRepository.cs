using BuildingBlocks.Commons.Interfaces;
using Meals.Entities;
using Meals.Features.Ingredients.Dtos;

namespace Meals.Features.Ingredients.Interfaces;

public interface IIngredientsRepository : IReadRepository<Ingredient>, IWriteRepository<Ingredient>
{
    Task<IEnumerable<IngredientsDto>> GetAllIngredients();
    Task<IngredientDetailsDto> GetIngredientById(string ingredientId);
    bool VerifyIngredientsByIds(IEnumerable<Guid> IngredientIds); 
}