using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Commons.Models;
using Meals.Commons.Interfaces;
using Meals.Features.Ingredients.Dtos;
using Meals.Features.Ingredients.Interfaces;

namespace Meals.Features.Ingredients.Queries.GetIngredientsByMealId;


sealed class GetIngredientsByMealIdQueryHandler : IQueryHandler<GetIngredientsByMealIdQuery, PaginatedResults<IngredientsDto>>
{
    private readonly IIngredientsRepository _ingredientsRepository;
    private readonly IMealsRepository _mealsRepository;
    public GetIngredientsByMealIdQueryHandler(IIngredientsRepository ingredientsRepository, IMealsRepository mealsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
        _mealsRepository = mealsRepository;
    }
    public async Task<PaginatedResults<IngredientsDto>> Handle(GetIngredientsByMealIdQuery request, CancellationToken cancellationToken)
    {
        // Verify if the Meal already exist in the records.
        var _ = await _mealsRepository.GetMealsById(request.MealId)
        ?? throw new NotFoundException($"Meal with Id '{request.MealId}' was not found.");

        var results = await _ingredientsRepository.GetPagedMealsIngredientsByMealId(
            request.MealId,
            request.Search,
            request.SortColumn,
            request.SortOrder,
            request.Page,
            request.PageSize
        );

        return results;
    }
}