using BuildingBlocks.EFCore;
using MealIngredients.API.Entities;
using MealIngredients.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MealIngredients.API.Repositories;

public sealed class MealIngredientsRepository : RepositoryBase<MealIngredient>, IMealIngredientsRepository
{
    public MealIngredientsRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task AddBulkMealIngredients(IEnumerable<MealIngredient> mealIngredients)
    {
        var query = _context.Set<MealIngredient>();

        await query.AddRangeAsync(mealIngredients);
        await _context.SaveChangesAsync();
    }
}