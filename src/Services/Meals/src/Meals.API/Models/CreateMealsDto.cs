namespace Meals.API.Models;

public class CreateMealsDto
{
    public required string MealName { get; set; }
    public string? MealReview { get; set; }
    public int Rating { get; set; }
    public required string CategoryId { get; set; }
}
