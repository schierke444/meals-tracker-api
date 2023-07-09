namespace Meals.Features.Meals.Dtos;

public sealed record UpdateMealDto(
    string MealName, 
    string? MealReview, 
    int Rating, 
    string Instructions, 
    ICollection<AddIngredientsToMealsDto> Ingredients, 
    ICollection<AddCategoryToMealsDto> Categories
); 