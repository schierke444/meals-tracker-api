using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.EFCore;
using Meals.Commons.Interfaces;
using Meals.Entities;
using Meals.Persistence;

namespace Meals.Repositories;

public sealed class MealIngredientsRepository : RepositoryBase<MealIngredients>, IMealIngredientsRepository
{
    private readonly IPgsqlDbContext _readDbContext;
    public MealIngredientsRepository(MealsDbContext context, IPgsqlDbContext readDbContext) : base(context)
    {
        _readDbContext = readDbContext;
    }
}