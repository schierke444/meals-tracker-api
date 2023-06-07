using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using Meals.Commons.Dtos;
using Meals.Commons.Interfaces;

namespace Meals.Features.Ingredients.Queries.GetIngredientById;

sealed class GetIngredientByIdQueryHandler : IQueryHandler<GetIngredientByIdQuery, IngredientDetailsDto>
{
    private readonly IIngredientsRepository _ingredientsRepository;

    public GetIngredientByIdQueryHandler(IIngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    public async Task<IngredientDetailsDto> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
    {
        var ingredient = await _ingredientsRepository.GetValue(
                       x => x.Id.ToString() == request.IngredientId,
                       x => new IngredientDetailsDto(x.Id, x.Name, x.CreatedAt, x.UpdatedAt)
                   )
            ?? throw new NotFoundException($"Ingredient with '{request.IngredientId}' was not found.");

        return ingredient;
    }
}