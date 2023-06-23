namespace Meals.Features.Ingredients.Dtos;

public sealed record IngredientDetailsDto(Guid Id, string Name, DateTime CreatedAt, DateTime UpdatedAt) : IngredientsDto(Id, Name);