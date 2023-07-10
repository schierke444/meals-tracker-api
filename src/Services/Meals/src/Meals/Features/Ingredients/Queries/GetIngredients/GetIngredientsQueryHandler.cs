using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Meals.Features.Ingredients.Dtos;
using Meals.Features.Ingredients.Interfaces;

namespace Meals.Features.Ingredients.Queries.GetIngredients;

sealed class GetIngredientsQueryHandler : IQueryHandler<GetIngredientsQuery, PaginatedResults<IngredientsDto>>
{
    private readonly IIngredientsRepository _ingredientsRepository;

    public GetIngredientsQueryHandler(IIngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    public async Task<PaginatedResults<IngredientsDto>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
    {
        var results = await _ingredientsRepository.GetPagedIngredientList(
            request.Search,
            request.SortColumn,
            request.SortOrder,
            request.Page,
            request.PageSize
        );

        return results; 
    }
}