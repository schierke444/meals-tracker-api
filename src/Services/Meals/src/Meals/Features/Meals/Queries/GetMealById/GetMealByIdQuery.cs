using BuildingBlocks.Commons.CQRS;
using Meals.Commons.Dtos;

namespace Meals.Features.Meals.Queries.GetMealById;

public record GetMealByIdQuery(string MealId) : IQuery<MealDetailsDto>;