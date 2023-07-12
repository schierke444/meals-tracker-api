using BuildingBlocks.Commons.CQRS;
using MediatR;

namespace Meals.Features.Likes.Commands.LikeMeal.v1;

public sealed record LikeMealCommand(string MealId) : ICommand<Unit>;