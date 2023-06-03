namespace Ingredients.API.Models;

public record IngredientDetailsDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}