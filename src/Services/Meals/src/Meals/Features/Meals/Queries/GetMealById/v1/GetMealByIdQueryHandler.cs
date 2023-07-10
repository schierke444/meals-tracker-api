using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using Meals.Commons.Interfaces;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Queries.GetMealById.v1;

sealed class GetMealByIdQueryHandler : IQueryHandler<GetMealByIdQuery, object>
{
    private readonly IMealsRepository _mealsRepository;

    public GetMealByIdQueryHandler(IMealsRepository mealsRepository)
    {
        _mealsRepository = mealsRepository;
    }

    public async Task<object> Handle(GetMealByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mealsRepository.GetMealsById(request.MealId)
         ?? throw new NotFoundException($"Meal with Id '{request.MealId}' was not found.");

        return result;
    }
}