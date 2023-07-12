using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Commons.Models;

namespace Meals.Entities;

[Table("liked_meals")]
public class LikedMeals : BaseEntity
{
    [Column("meal_id")]
    public Guid MealId { get; set; }
    public Meal? Meal { get; set; }
    [Column("owner_id")] 
    public Guid OwnerId { get; set; }
    public UsersMeals? UsersMeals { get; set; }
}