using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events;
using Ingredients.API.Repositories;
using MassTransit;

namespace Ingredients.API.RequestConsumers;

public class VerifyIngredientsByIdConsumer : IConsumer<VerifyIngredientByIdRecord> {
    
    private readonly IIngredientsRepository _ingredientsRepository;

    public VerifyIngredientsByIdConsumer(IIngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    public async Task Consume(ConsumeContext<VerifyIngredientByIdRecord> context)
    {
        var request = context.Message;
        foreach (var id in request.Ingredients)
        {
            var result = await _ingredientsRepository.GetValue(x => x.Id.ToString() == id.ToString()) 
                ?? throw new NotFoundException($"Ingredient with Id '{id}' was not found.");
        }

        await context.RespondAsync<VerifyIngredientByIdResponse>(new VerifyIngredientByIdResponse());
    }
}