namespace Meals.API.Models;

public record CreateMealsDto
{
    public required string MealName { get; init; }
    public string? MealReview { get; init; }
    public int Rating { get; init; }
    public required string CategoryId { get; init; }
    public required IEnumerable<Guid> Ingredients {get; init;}
}
