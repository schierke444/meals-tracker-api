namespace Meals.Features.Meals.Dtos;

public record MealsDto(Guid Id, string MealName, string? MealReview, int Rating, DateTime CreatedAt)
{
   public UserDetailsDto? Owner { get; set; } 
}
