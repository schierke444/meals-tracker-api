using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using Meals.Entities;
using Meals.Features.Ingredients.Dtos;

namespace Meals.Features.Ingredients.Interfaces;

public interface IIngredientsRepository : IReadRepository<Ingredient>, IWriteRepository<Ingredient>
{
    Task<PaginatedResults<IngredientDetailsDto>> GetPagedIngredientList(
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

    Task<IngredientDetailsDto> GetIngredientById(string ingredientId);
    bool VerifyIngredientsByIds(IEnumerable<Guid> IngredientIds); 
}