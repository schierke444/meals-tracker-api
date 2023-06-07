using BuildingBlocks.Commons.Interfaces;
using Meals.Entities;

namespace Meals.Commons.Interfaces;

public interface IIngredientsRepository : IRepositoryBase<Ingredient>
{
    bool VerifyIngredientsByIds(IEnumerable<Guid> IngredientIds); 
}