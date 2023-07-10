using BuildingBlocks.Commons.CQRS;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Queries.GetMealById.v1;

public record GetMealByIdQuery(string MealId) : IQuery<object>;