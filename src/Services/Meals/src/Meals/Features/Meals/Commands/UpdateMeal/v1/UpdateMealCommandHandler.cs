using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Services;
using FluentValidation;
using Meals.Commons.Interfaces;
using Meals.Features.Category;
using Meals.Features.Ingredients.Interfaces;
using Meals.Features.Meals.Dtos;
using Meals.Features.Meals.Interfaces;
using Meals.Features.Meals.Services;
using MediatR;
using ValidationException = BuildingBlocks.Commons.Exceptions.ValidationException;

namespace Meals.Features.Meals.Commands.UpdateMeal.v1;

sealed class UpdateMealCommandHandler : ICommandHandler<UpdateMealCommand, Unit>
{
    private readonly IMealsRepository _mealsRepository;
    private readonly IIngredientsRepository _ingredientsRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMealCategoryRepository _mealCategoryRepository;
    private readonly IMealIngredientsRepository _mealIngredientsRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<UpdateMealDto> _validator;
    private readonly MealCategoryService _mealCategoryService;
    private readonly MealIngredientsService _mealIngredientsService;
    public UpdateMealCommandHandler(IMealsRepository mealsRepository, ICurrentUserService currentUserService, IMealCategoryRepository mealCategoryRepository, IMealIngredientsRepository mealIngredientsRepository, IIngredientsRepository ingredientsRepository, ICategoryRepository categoryRepository, IValidator<UpdateMealDto> validator, MealCategoryService mealCategoryService, MealIngredientsService mealIngredientsService)
    {
        _mealsRepository = mealsRepository;
        _currentUserService = currentUserService;
        _mealCategoryRepository = mealCategoryRepository;
        _mealIngredientsRepository = mealIngredientsRepository;
        _ingredientsRepository = ingredientsRepository;
        _categoryRepository = categoryRepository;
        _validator = validator;
        _mealCategoryService = mealCategoryService;
        _mealIngredientsService = mealIngredientsService;
    }
    public async Task<Unit> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
    {
        var meal = await _mealsRepository.GetValue(
            x => x.Id.ToString() == request.MealId &&
            x.OwnerId.ToString() == _currentUserService.UserId,
            AsNoTracking: false
        )
            ?? throw new NotFoundException($"Meal with Id '{request.MealId}' was not found.");
        
        // Validate Fields
        var validationResults = await _validator.ValidateAsync(request.Meal);

        if(!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);

        var ingredientResult = _ingredientsRepository.VerifyIngredientsByIds(request.Meal.Ingredients);
        if(!ingredientResult)
            throw new ConflictException("Invalid Ingredient Ids");

        var categoryResult = _categoryRepository.VerifyCategoryByIds(request.Meal.Categories);
        if(!ingredientResult)
            throw new ConflictException("Invalid Category Ids");


        // Update Fields
        meal.MealName = request.Meal.MealName;
        meal.MealReview = request.Meal.MealReview;
        meal.Rating = request.Meal.Rating;
        meal.Instructions = request.Meal.Instructions;

        await _mealsRepository.SaveChangesAsync(cancellationToken);

        // Delete all records of many-to-many in category and ingredients
        var mealIngredientsResults = await _mealIngredientsRepository.GetAllValuesByExp(
            x => x.MealId == meal.Id,
            null
        );
        _mealIngredientsRepository.DeleteRange(mealIngredientsResults);
        await _mealIngredientsRepository.SaveChangesAsync(cancellationToken);


        var mealCategoriesResults = await _mealCategoryRepository.GetAllValuesByExp(
            x => x.MealId == meal.Id,
            null
        );
        _mealCategoryRepository.DeleteRange(mealCategoriesResults);
        await _mealCategoryRepository.SaveChangesAsync(cancellationToken);

        var mealIngredients = _mealIngredientsService.CreateMealWithIngredients(meal.Id, request.Meal.Ingredients);
        var mealCategories = _mealCategoryService.CreateMealWithCategory(meal.Id, request.Meal.Categories);

        await _mealIngredientsRepository.AddRange(mealIngredients);
        await _mealCategoryRepository.AddRange(mealCategories);

        await _mealIngredientsRepository.SaveChangesAsync(cancellationToken);
        await _mealCategoryRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}