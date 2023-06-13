namespace Meals.Features.Meals.Dtos;

public sealed record MealDetailsDto(Guid Id, string MealName, string? MealReview, int Rating, Guid CategoryId, Guid OwnerId, DateTime CreatedAt, DateTime UpdatedAt)
 : MealsDto(Id, MealName, MealReview, Rating, CreatedAt)
{

}