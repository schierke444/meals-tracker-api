using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Commons.Models;

namespace Meals.Entities;

[Table("meal_ingredients")]
public class MealIngredients : BaseEntity
{
    [Column("ingredient_id")]
    public Guid IngredientId { get; set; } 
    public Ingredient? Ingredients { get; set; }
    [Column("meal_id")]
    public Guid MealId { get; set; }
    public Meal? Meals { get; set; }
}