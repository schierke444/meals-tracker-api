namespace Ingredients.API.Models;

public sealed class IngredientsDto
{
    public Guid Id { get; set; }
    public required string Name{ get; set; }
}