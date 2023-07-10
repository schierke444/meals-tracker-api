using Meals.Entities;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Services;

public class MealCategoryService
{
    public MealCategoryService()
    {
        
    }
    public ICollection<MealCategory> CreateMealWithCategory(Guid MealId, IEnumerable<AddCategoryToMealsDto> Categories)
    {
        ICollection<MealCategory> mealCategories = new List<MealCategory>();

        foreach (var item in Categories)
        {
           mealCategories.Add(new MealCategory{ 
                Id = Guid.NewGuid(),
                MealId = MealId,
                CategoryId = item.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }
        return mealCategories;
    }
}