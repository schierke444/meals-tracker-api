using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using FluentValidation;
using Meals.Features.Ingredients.Dtos;
using Meals.Features.Ingredients.Interfaces;
using MediatR;
using ValidationException = BuildingBlocks.Commons.Exceptions.ValidationException;

namespace Meals.Features.Ingredients.Commands.UpdateIngredient.v1;

sealed class UpdateIngredientCommandHandler : ICommandHandler<UpdateIngredientCommand, Unit>
{
    private readonly IIngredientsRepository _ingredientsRepository;
    private readonly IValidator<UpdateIngredientDto> _validator;
    public UpdateIngredientCommandHandler(IIngredientsRepository ingredientsRepository, IValidator<UpdateIngredientDto> validator)
    {
        _ingredientsRepository = ingredientsRepository;
        _validator = validator;
    }
    public async Task<Unit> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
    {
        var ingredient = await _ingredientsRepository.GetValue(
            x => x.Id.ToString() == request.IngredientId, AsNoTracking: false)
            ?? throw new NotFoundException($"Ingredient with Id '{request.IngredientId}' was not found.");

        UpdateIngredientDto ingredientToUpdate = new(ingredient.Name);

        request.UpdateIngredient.ApplyTo(ingredientToUpdate, (err) => {
            throw new ConflictException("Error in JsonPatchDocument: " + err.ErrorMessage);
        });

        var existingIngredient = await _ingredientsRepository.GetValue(
            x => x.Name.ToLower() == ingredientToUpdate.Name,
            x => new {x.Id}
        );

        if(existingIngredient is not null) 
            throw new ConflictException($"Ingredient with Name '{ingredientToUpdate.Name}' already exists.");

        var validationResults = await _validator.ValidateAsync(ingredientToUpdate, cancellationToken);

        if(!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);

        ingredient.Name = ingredientToUpdate.Name;

        await _ingredientsRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}