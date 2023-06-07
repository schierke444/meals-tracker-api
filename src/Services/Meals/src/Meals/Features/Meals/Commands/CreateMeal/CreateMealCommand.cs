using BuildingBlocks.Commons.CQRS;

namespace Meals.Features.Meals.Commands.CreateMeal;

public sealed record CreateMealCommand : ICommand<Guid>
{
    public required string MealName { get; init; }
    public string? MealReview { get; init; }
    public int Rating { get; init; }
    public required string CategoryId { get; init; }
    public required IEnumerable<Guid> Ingredients {get; init;}
}