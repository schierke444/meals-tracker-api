using BuildingBlocks.Commons.CQRS;
using Meals.Commons.Dtos;
using Meals.Commons.Interfaces;

namespace Meals.Features.Ingredients.Queries.GetIngredients;

sealed class GetIngredientsQueryHandler : IQueryHandler<GetIngredientsQuery, IEnumerable<IngredientsDto>>
{
    private readonly IIngredientsRepository _ingredientsRepository;

    public GetIngredientsQueryHandler(IIngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    public async Task<IEnumerable<IngredientsDto>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
    {
        var results = await _ingredientsRepository.GetAllValues(
                x => new IngredientsDto(x.Id, x.Name),
                null,
                true
            );

        return results; 
    }
}