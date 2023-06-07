using BuildingBlocks.Commons.CQRS;

namespace Meals.Features.Ingredients.Commands.CreateIngredient;

public record CreateIngredientCommand(string Name) : ICommand<Guid>;