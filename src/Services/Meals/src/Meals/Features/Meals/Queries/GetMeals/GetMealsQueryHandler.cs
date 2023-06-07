using BuildingBlocks.Commons.CQRS;
using Meals.Commons.Dtos;
using Meals.Commons.Interfaces;

namespace Meals.Features.Meals.Queries.GetMeals;

sealed class GetMealsQueryHandler : IQueryHandler<GetMealsQuery, IEnumerable<MealsDto>>
{
    private readonly IMealsRepository _mealsRepository;

    public GetMealsQueryHandler(IMealsRepository mealsRepository)
    {
        _mealsRepository = mealsRepository;
    }

    public async Task<IEnumerable<MealsDto>> Handle(GetMealsQuery request, CancellationToken cancellationToken)
    {
        var results = await _mealsRepository.GetAllValues(
            x => new MealsDto(x.Id, x.MealName, x.MealReview, x.Rating),
            null,
            true
        );

        return results;
    }
}