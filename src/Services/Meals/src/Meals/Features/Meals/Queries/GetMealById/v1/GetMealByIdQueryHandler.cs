using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using Meals.Commons.Interfaces;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Queries.GetMealById.v1;

sealed class GetMealByIdQueryHandler : IQueryHandler<GetMealByIdQuery, MealDetailsDto>
{
    private readonly IMealsRepository _mealsRepository;

    public GetMealByIdQueryHandler(IMealsRepository mealsRepository)
    {
        _mealsRepository = mealsRepository;
    }

    public async Task<MealDetailsDto> Handle(GetMealByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mealsRepository.GetMealsById(request.MealId, request.includeIngredients, request.includeCategory)
         ?? throw new NotFoundException($"Meal with Id '{request.MealId}' was not found.");

        return result;
    }
}