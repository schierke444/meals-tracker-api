using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Meals.Commons.Interfaces;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Queries.GetMealsByOwnerId.v1;

sealed class GetMealsByOwnerIdQueryHandler : IQueryHandler<GetMealsByOwnerIdQuery, PaginatedResults<MealsDto>>
{
    private readonly IMealsRepository _mealsRepository;
    public GetMealsByOwnerIdQueryHandler(IMealsRepository mealsRepository)
    {
        _mealsRepository = mealsRepository;
    }

    public async Task<PaginatedResults<MealsDto>> Handle(GetMealsByOwnerIdQuery request, CancellationToken cancellationToken)
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