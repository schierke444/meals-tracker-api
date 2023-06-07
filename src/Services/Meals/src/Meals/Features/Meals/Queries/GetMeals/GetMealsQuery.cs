using BuildingBlocks.Commons.CQRS;
using Meals.Commons.Dtos;

namespace Meals.Features.Meals.Queries.GetMeals;

public record GetMealsQuery : IQuery<IEnumerable<MealsDto>>
{
    
}