using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Queries.GetMealsByOwnerId;

public record GetMealsByOwnerIdQuery(string OwnerId, int Page, int PageSize) : IQuery<PaginatedResults<MealsDto>>;