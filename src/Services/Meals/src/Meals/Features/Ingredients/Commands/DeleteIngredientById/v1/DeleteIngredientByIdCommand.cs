using MediatR;

namespace Meals.Features.Ingredients.Commands.DeleteIngredientById.v1;

public sealed record DeleteIngredientByIdCommand(string IngredientId) : IRequest;