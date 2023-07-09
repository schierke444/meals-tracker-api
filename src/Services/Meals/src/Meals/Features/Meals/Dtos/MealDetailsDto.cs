using Category.Features.Dtos;
using Meals.Features.Ingredients.Dtos;

namespace Meals.Features.Meals.Dtos;

public sealed record MealDetailsDto (
    Guid Id, 
    string MealName, 
    string? MealReview, 
    int Rating, 
    string Instructions, 
    IEnumerable<IngredientsWithAmountDto> Ingredients, 
    IEnumerable<CategoryDto> Category, 
    UserDetailsDto Owner, 
    DateTime CreatedAt
);