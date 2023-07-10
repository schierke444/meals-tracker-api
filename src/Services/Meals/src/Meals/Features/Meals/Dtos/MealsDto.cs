namespace Meals.Features.Meals.Dtos;

public record MealsDto(Guid Id, string MealName, int Rating, DateTime CreatedAt);