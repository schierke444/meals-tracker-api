using BuildingBlocks.Commons.Models;

namespace MealIngredients.API.Entities;

public sealed class MealIngredient : BaseEntity
{
    public Guid MealId { get; set; }
    public Guid IngredientId { get; set; }
    public Guid UserId { get; set; }
}