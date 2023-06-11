namespace Meals.Features.Meals.Dtos;

public sealed record MealDetailsDto(Guid Id, string Meal_Name, string? Meal_Review, int Rating, Guid Category_Id, Guid Owner_Id, DateTime Created_At, DateTime Updated_At)
 : MealsDto(Id, Meal_Name, Meal_Review, Rating, Created_At)
{

}