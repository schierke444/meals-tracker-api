using AutoMapper;
using BuildingBlocks.Events;
using MassTransit;
using MealIngredients.API.Entities;
using MealIngredients.API.Repositories;

namespace MealIngredients.API.Consumers;

public sealed class CreateMealAndIngredientConsumer : IConsumer<CreateMealEvent>
{
    private readonly IMealIngredientsRepository _mealIngredientsRepository;
    private readonly IMapper _mapper;
    public CreateMealAndIngredientConsumer(IMealIngredientsRepository mealIngredientsRepository, IMapper mapper)
    {
        _mealIngredientsRepository = mealIngredientsRepository;
        _mapper = mapper;
    }

    public Task Consume(ConsumeContext<CreateMealEvent> context)
    {
        var mealIngredients = context.Message;

        var mappedMealIngredients = _mapper.Map<IEnumerable<MealIngredient>>(mealIngredients.MealIngredients);

        _mealIngredientsRepository.AddBulkMealIngredients(mappedMealIngredients); 
        
        return Task.CompletedTask;
    }
}