using Meals.Entities;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Services;

public class MealIngredientsService
{
    public ICollection<MealIngredients> CreateMealWithIngredients(Guid MealId, IEnumerable<AddIngredientsToMealsDto> Ingredients)
    {
        ICollection<MealIngredients> mealIngredients = new List<MealIngredients>();

        foreach (var item in Ingredients)
        {
            mealIngredients.Add(new MealIngredients
            {
                Id = Guid.NewGuid(),
                MealId = MealId,
                IngredientId = item.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Amount = item.Amount
            });
        }
        return mealIngredients;
    }
}