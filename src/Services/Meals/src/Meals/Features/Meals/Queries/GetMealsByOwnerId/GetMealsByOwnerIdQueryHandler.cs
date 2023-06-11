using BuildingBlocks.Commons.CQRS;
using Meals.Commons.Interfaces;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Queries.GetMealsByOwnerId;

sealed class GetMealsByOwnerIdQueryHandler : IQueryHandler<GetMealsByOwnerIdQuery, IEnumerable<MealsDto>>
{
    private readonly IMealsRepository _mealsRepository;
    public GetMealsByOwnerIdQueryHandler(IMealsRepository mealsRepository)
    {
        _mealsRepository = mealsRepository;
    }

    public async Task<IEnumerable<MealsDto>> Handle(GetMealsByOwnerIdQuery request, CancellationToken cancellationToken)
    {
        var results = await _mealsRepository.GetAllMealsByOwnerId(request.OwnerId);

        return results;
    }
}