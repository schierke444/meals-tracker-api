using MediatR;

namespace Meals.Features.Ingredients.Commands.DeleteIngredientById;

public sealed record DeleteIngredientByIdCommand(string IngredientId) : IRequest;