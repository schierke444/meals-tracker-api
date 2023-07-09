using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using Meals.Entities;
using Meals.Features.Ingredients.Dtos;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Ingredients.Interfaces;

public interface IIngredientsRepository : IReadRepository<Ingredient>, IWriteRepository<Ingredient>
{
    Task<PaginatedResults<IngredientsDto>> GetPagedIngredientList(
        string? search,
        string? sortColumn,
        string? sortOrder,
        int page = 1,
        int pageSize = 10
    );

    Task<PaginatedResults<IngredientsDto>> GetPagedMealsIngredientsByMealId(
        string MealId, 
        string? search,  
        string? sortColumn,
        string? sortOrder,
        int page = 1, 
        int pageSize =10);

    bool VerifyIngredientsByIds(IEnumerable<AddIngredientsToMealsDto> IngredientIds); 
}