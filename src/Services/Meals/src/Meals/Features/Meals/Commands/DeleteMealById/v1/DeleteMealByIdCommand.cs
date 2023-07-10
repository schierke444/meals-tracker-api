using MediatR;

namespace Meals.Features.Meals.Commands.DeleteMealById.v1;

public record DeleteMealByIdCommand(string MealId) : IRequest;