using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Commons.Models;

namespace Meals.Entities;

[Table("meals")]
public class Meal : BaseEntity
{
    [Column("meal_name")]
    public required string MealName { get; set; }
    [Column("meal_review")]
    public string? MealReview { get; set; }
    [Column("rating")]
    public int Rating { get; set; }
    [Column("owner_id")]
    public Guid OwnerId { get; set; }
    [Column("category_id")]
    public Guid CategoryId { get; set; }
    public ICollection<MealIngredients> MealIngredient { get; set; } = new List<MealIngredients>();
}
