using BuildingBlocks.Commons.Models;
using BuildingBlocks.Commons.Models.EventModels;

namespace BuildingBlocks.Events;

public sealed class CreateMealEvent : BaseEvent
{
    public ICollection<CreateMealEventDto> MealIngredients {get; set;} = new List<CreateMealEventDto>();
}