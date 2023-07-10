using BuildingBlocks.Commons.Exceptions;
using Meals.Features.Ingredients.Interfaces;
using MediatR;

namespace Meals.Features.Ingredients.Commands.DeleteIngredientById.v1;

sealed class DeleteIngredientByIdCommandHandler : IRequestHandler<DeleteIngredientByIdCommand>
{
    private readonly IIngredientsRepository _ingredientsRepository;

    public DeleteIngredientByIdCommandHandler(IIngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    public async Task Handle(DeleteIngredientByIdCommand request, CancellationToken cancellationToken)
    {
        var existingIngredient = await _ingredientsRepository.GetValue(
                           x => x.Id.ToString() == request.IngredientId 
                       )
        ?? throw new NotFoundException($"Ingredient with Id '{request.IngredientId}' was not found.");

        _ingredientsRepository.Delete(existingIngredient);
        await _ingredientsRepository.SaveChangesAsync(cancellationToken);
    }
}