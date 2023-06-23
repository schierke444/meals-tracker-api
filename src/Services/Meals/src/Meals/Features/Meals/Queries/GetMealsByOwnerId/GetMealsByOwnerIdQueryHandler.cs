using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Meals.Commons.Interfaces;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Queries.GetMealsByOwnerId;

sealed class GetMealsByOwnerIdQueryHandler : IQueryHandler<GetMealsByOwnerIdQuery, PaginatedResults<MealDetailsDto>>
{
    private readonly IMealsRepository _mealsRepository;
    public GetMealsByOwnerIdQueryHandler(IMealsRepository mealsRepository)
    {
        _mealsRepository = mealsRepository;
    }

    public async Task<PaginatedResults<MealDetailsDto>> Handle(GetMealsByOwnerIdQuery request, CancellationToken cancellationToken)
    {
        var results = await _mealsRepository.GetPagedMealsListByOwnerId(
            request.OwnerId, 
            request.Search,
            request.SortColumn,
            request.SortOrder,
            request.Page, 
            request.PageSize);


        return results;
    }
}