namespace Meals.Commons.Dtos;

public record MealsDto(Guid Id, string MealName, string? MealReview, int Rating);