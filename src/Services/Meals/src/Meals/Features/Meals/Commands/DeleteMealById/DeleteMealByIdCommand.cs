using MediatR;

namespace Meals.Features.Meals.Commands.DeleteMealById;

public record DeleteMealByIdCommand(string MealId) : IRequest;