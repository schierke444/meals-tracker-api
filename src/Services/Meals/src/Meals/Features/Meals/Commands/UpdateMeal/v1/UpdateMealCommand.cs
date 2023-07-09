using BuildingBlocks.Commons.CQRS;
using Meals.Features.Meals.Dtos;
using MediatR;

namespace Meals.Features.Meals.Commands.UpdateMeal.v1;

public sealed record UpdateMealCommand(string MealId, UpdateMealDto Meal) : ICommand<Unit>;