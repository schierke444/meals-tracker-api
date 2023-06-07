using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using Meals.Commons.Interfaces;
using Meals.Entities;

namespace Meals.Features.Ingredients.Commands.CreateIngredient;

sealed class CreateIngredientCommandHandler : ICommandHandler<CreateIngredientCommand, Guid>
{
    private readonly IIngredientsRepository _ingredientsRepository;

    public CreateIngredientCommandHandler(IIngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    public async Task<Guid> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
    {
        var existingIngredient = await _ingredientsRepository.GetValue(
            x => x.Name.ToLower() == request.Name.ToLower()
        );

        if (existingIngredient != null)
            throw new ConflictException($"Ingredient '{request.Name}' already exist.");

        Ingredient newIngredient = new()
        {
            Name = request.Name
        };

        await _ingredientsRepository.Create(newIngredient);
        await _ingredientsRepository.SaveChangesAsync(cancellationToken);

        return newIngredient.Id;
    }
}