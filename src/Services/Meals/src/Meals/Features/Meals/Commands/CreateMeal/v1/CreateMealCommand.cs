using BuildingBlocks.Commons.CQRS;
using Meals.Features.Ingredients.Dtos;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Commands.CreateMeal.v1;

public sealed record CreateMealCommand : ICommand<Guid>
{
    public required string MealName { get; init; }
    public string? MealReview { get; init; }
    public int Rating { get; init; }
    public required string Instructions { get; init; }
    public required ICollection<AddIngredientsToMealsDto> Ingredients { get; init; }
    public required ICollection<AddCategoryToMealsDto> Categories { get; init; }
}