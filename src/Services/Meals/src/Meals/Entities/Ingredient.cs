using BuildingBlocks.Commons.Models;

namespace Meals.Entities;

public class Ingredient : BaseEntity 
{
    public required string Name { get; set; }
    public ICollection<MealIngredients> MealIngredient { get; set; } = new List<MealIngredients>();
}