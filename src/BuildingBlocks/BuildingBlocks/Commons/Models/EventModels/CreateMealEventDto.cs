namespace BuildingBlocks.Commons.Models.EventModels;

public record CreateMealEventDto (Guid MealId, Guid IngredientId, Guid UserId)
{
}