using BuildingBlocks.Commons.Interfaces;
using Meals.Entities;

namespace Meals.Commons.Interfaces;

public interface IMealIngredientsRepository : IReadRepository<MealIngredients>, IWriteRepository<MealIngredients>
{
    
}