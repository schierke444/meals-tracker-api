using BuildingBlocks.Commons.CQRS;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Queries.GetMealsByOwnerId;

public record GetMealsByOwnerIdQuery(string OwnerId) : IQuery<IEnumerable<MealsDto>>;