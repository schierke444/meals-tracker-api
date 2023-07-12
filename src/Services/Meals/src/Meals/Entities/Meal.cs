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
    [Column("instructions")]
    public required string Instructions { get; set; }
    [Column("owner_id")]
    public Guid OwnerId { get; set; }
    public UsersMeals? UsersMeals { get; set; }
    public ICollection<MealCategory> MealCategories {get; set; } = new List<MealCategory>();
    public ICollection<MealIngredients> MealIngredient { get; set; } = new List<MealIngredients>();
    public ICollection<LikedMeals> Likes { get; set; } = new List<LikedMeals>();
}
