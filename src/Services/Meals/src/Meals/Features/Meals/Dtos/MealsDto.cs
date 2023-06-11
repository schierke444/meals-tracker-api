namespace Meals.Features.Meals.Dtos;

public record MealsDto(Guid Id, string Meal_Name, string? Meal_Review, int Rating, DateTime Created_At);
