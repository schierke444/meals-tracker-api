using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using Meals.Commons.Dtos;
using Meals.Commons.Interfaces;

namespace Meals.Features.Meals.Queries.GetMealById;

sealed class GetMealByIdQueryHandler : IQueryHandler<GetMealByIdQuery, MealDetailsDto>
{
    private readonly IMealsRepository _mealsRepository;

    public GetMealByIdQueryHandler(IMealsRepository mealsRepository)
    {
        _mealsRepository = mealsRepository;
    }

    public async Task<MealDetailsDto> Handle(GetMealByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mealsRepository.GetValue(
            x => x.Id.ToString() == request.MealId,
            x => new MealDetailsDto(x.Id, x.MealName, x.MealReview, x.Rating, x.CategoryId, x.OwnerId)
        ) ?? throw new NotFoundException($"Meal with Id '{request.MealId}' was not found.");

        return result;
    }
}