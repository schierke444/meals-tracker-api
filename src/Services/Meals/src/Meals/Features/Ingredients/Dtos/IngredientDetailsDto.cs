namespace Meals.Features.Ingredients.Dtos;

public sealed record IngredientDetailsDto(Guid Id, string Name, DateTime Created_At, DateTime Updated_At) : IngredientsDto(Id, Name);