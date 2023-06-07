using BuildingBlocks.Commons.Models;

namespace Meals.Entities;

public class Meal : BaseEntity
{
    public required string MealName { get; set; }
    public string? MealReview { get; set; }
    public int Rating { get; set; }
    public Guid OwnerId { get; set; }
    public Guid CategoryId { get; set; }
    public ICollection<MealIngredients> MealIngredient { get; set; } = new List<MealIngredients>();
}
