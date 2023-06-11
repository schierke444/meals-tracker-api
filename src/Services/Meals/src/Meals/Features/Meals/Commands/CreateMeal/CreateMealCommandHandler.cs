using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Services;
using Meals.Commons.Interfaces;
using Meals.Entities;
using Meals.Features.Ingredients.Interfaces;

namespace Meals.Features.Meals.Commands.CreateMeal;

sealed class CreateMealCommandHandler : ICommandHandler<CreateMealCommand, Guid>
{
    private readonly IMealsRepository _mealsRepository;
    private readonly IIngredientsRepository _ingredientsRepository;
    private readonly IMealIngredientsRepository _mealIngredientsRepository;
    private readonly ICurrentUserService _currentUserService;
    public CreateMealCommandHandler(IMealsRepository mealsRepository, ICurrentUserService currentUserService, IIngredientsRepository ingredientsRepository, IMealIngredientsRepository mealIngredientsRepository)
    {
        _mealsRepository = mealsRepository;
        _currentUserService = currentUserService;
        _ingredientsRepository = ingredientsRepository;
        _mealIngredientsRepository = mealIngredientsRepository;
    }

    public async Task<Guid> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
        var result = _ingredientsRepository.VerifyIngredientsByIds(request.Ingredients);
        if(!result)
            throw new ConflictException("Invalid Ingredient Ids");
        
        Meal newMeal = new()
        {
            MealName = request.MealName,
            MealReview = request.MealReview,
            Rating = request.Rating,
            CategoryId = Guid.Parse(request.CategoryId),
            OwnerId = Guid.Parse(_currentUserService.UserId ?? throw new UnauthorizedAccessException())
        };

        await _mealsRepository.Add(newMeal);
        await _mealsRepository.SaveChangesAsync(cancellationToken);

        var mealIngredients = CreateMealWithIngredients(newMeal.Id, request.Ingredients);
        await _mealIngredientsRepository.AddRange(mealIngredients);
        await _mealIngredientsRepository.SaveChangesAsync(cancellationToken);

        return newMeal.Id;
    }
    
    private ICollection<MealIngredients> CreateMealWithIngredients(Guid MealId, IEnumerable<Guid> Ingredients)
    {
        ICollection<MealIngredients> mealIngredients = new List<MealIngredients>();

        foreach (var item in Ingredients)
        {
           mealIngredients.Add(new MealIngredients { 
                Id = Guid.NewGuid(),
                MealId = MealId,
                IngredientId = item,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        return mealIngredients;
    }

    
}