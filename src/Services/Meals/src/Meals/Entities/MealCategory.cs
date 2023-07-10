using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Commons.Models;

namespace Meals.Entities;

[Table("meal_category")]
public sealed class MealCategory : BaseEntity
{
    [Column("meal_id")]
    public required Guid MealId { get; set; }
    public Meal? Meal { get; set; } 
    [Column("category_id")]
    public required Guid CategoryId { get; set; }
    public Category? Category { get; set; } 
}