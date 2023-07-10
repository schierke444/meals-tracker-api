using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using MassTransit;
using Meals.Commons.Interfaces;
using Meals.Entities;
using Meals.Features.Category;
using Meals.Features.Ingredients.Interfaces;
using Meals.Features.Meals.Interfaces;
using Meals.Features.Meals.Services;

namespace Meals.Features.Meals.Commands.CreateMeal.v1;

sealed class CreateMealCommandHandler : ICommandHandler<CreateMealCommand, Guid>
{
    private readonly IMealsRepository _mealsRepository;
    private readonly IIngredientsRepository _ingredientsRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMealIngredientsRepository _mealIngredientsRepository;
    private readonly IMealCategoryRepository _mealCategoryRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IRequestClient<GetUserByIdRecord> _client;
    private readonly MealCategoryService _mealCategoryService;
    private readonly MealIngredientsService _mealIngredientsService;
    public CreateMealCommandHandler(IMealsRepository mealsRepository, ICurrentUserService currentUserService, IIngredientsRepository ingredientsRepository, IMealIngredientsRepository mealIngredientsRepository, IMealCategoryRepository mealCategoryRepository, IRequestClient<GetUserByIdRecord> client, ICategoryRepository categoryRepository, MealCategoryService mealCategoryService, MealIngredientsService mealIngredientsService)
    {
        _mealsRepository = mealsRepository;
        _currentUserService = currentUserService;
        _ingredientsRepository = ingredientsRepository;
        _mealIngredientsRepository = mealIngredientsRepository;
        _mealCategoryRepository = mealCategoryRepository;
        _client = client;
        _categoryRepository = categoryRepository;
        _mealCategoryService = mealCategoryService;
        _mealIngredientsService = mealIngredientsService;
    }

    public async Task<Guid> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
        var user = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(_currentUserService.UserId ?? throw new UnauthorizedAccessException()));

        var ingredientResult = _ingredientsRepository.VerifyIngredientsByIds(request.Ingredients);
        if(!ingredientResult)
            throw new ConflictException("Invalid Ingredient Ids");


        var categoryResult = _categoryRepository.VerifyCategoryByIds(request.Categories);
        if(!ingredientResult)
            throw new ConflictException("Invalid Category Ids");
        
        Meal newMeal = new()
        {
            MealName = request.MealName,
            MealReview = request.MealReview,
            Rating = request.Rating,
            Instructions = request.Instructions,
            OwnerId = user.Message.Id,
            OwnerName = user.Message.Username
        };

        await _mealsRepository.Add(newMeal);
        await _mealsRepository.SaveChangesAsync(cancellationToken);

        var mealIngredients = _mealIngredientsService.CreateMealWithIngredients(newMeal.Id, request.Ingredients);
        var mealCategories = _mealCategoryService.CreateMealWithCategory(newMeal.Id, request.Categories);

        await _mealIngredientsRepository.AddRange(mealIngredients);
        await _mealCategoryRepository.AddRange(mealCategories);

        await _mealIngredientsRepository.SaveChangesAsync(cancellationToken);
        await _mealCategoryRepository.SaveChangesAsync(cancellationToken);

        return newMeal.Id;
    }
}