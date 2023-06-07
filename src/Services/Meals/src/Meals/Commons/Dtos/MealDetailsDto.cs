namespace Meals.Commons.Dtos; 

public sealed record MealDetailsDto(Guid Id, string MealName, string? MealReview, int Rating, Guid CategoryId, Guid OwnerId)
 : MealsDto(Id, MealName, MealReview, Rating)
{

}