using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Meals.Commons.Interfaces;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Queries.GetMealsByOwnerId;

sealed class GetMealsByOwnerIdQueryHandler : IQueryHandler<GetMealsByOwnerIdQuery, PaginatedResults<MealsDto>>
{
    private readonly IMealsRepository _mealsRepository;
    public GetMealsByOwnerIdQueryHandler(IMealsRepository mealsRepository)
    {
        _mealsRepository = mealsRepository;
    }

    public async Task<PaginatedResults<MealsDto>> Handle(GetMealsByOwnerIdQuery request, CancellationToken cancellationToken)
    {
        var results = await _mealsRepository.GetAllMealsByOwnerId(request.OwnerId, request.Page, request.PageSize);
        var totalMealsCount = await _mealsRepository.GetMealsCountByOwnerId(request.OwnerId);

        PageMetadata p = new(request.Page, request.PageSize, totalMealsCount);

        return new PaginatedResults<MealsDto>(results, p);
    }
}