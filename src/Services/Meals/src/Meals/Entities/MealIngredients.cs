using BuildingBlocks.Commons.Models;

namespace Meals.Entities;

public class MealIngredients : BaseEntity
{
    public Guid IngredientId { get; set; } 
    public Ingredient? Ingredients { get; set; }
    public Guid MealId { get; set; }
    public Meal? Meals { get; set; }
}