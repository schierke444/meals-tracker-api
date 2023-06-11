using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Commons.Models;

namespace Meals.Entities;

[Table("ingredients")]
public class Ingredient : BaseEntity 
{
    [Column("name")]
    public required string Name { get; set; }
    public ICollection<MealIngredients> MealIngredient { get; set; } = new List<MealIngredients>();
}