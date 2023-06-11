using BuildingBlocks.Commons.CQRS;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Queries.GetMealById;

public record GetMealByIdQuery(string MealId) : IQuery<MealDetailsDto>;