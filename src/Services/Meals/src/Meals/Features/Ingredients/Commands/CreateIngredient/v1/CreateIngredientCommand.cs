using BuildingBlocks.Commons.CQRS;

namespace Meals.Features.Ingredients.Commands.CreateIngredient.v1;

public record CreateIngredientCommand(string Name) : ICommand<Guid>;