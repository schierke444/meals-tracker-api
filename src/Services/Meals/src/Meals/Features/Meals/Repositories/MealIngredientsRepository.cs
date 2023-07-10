using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.EFCore;
using Meals.Entities;
using Meals.Features.Meals.Interfaces;
using Meals.Persistence;

namespace Meals.Features.Meals.Repositories;

public sealed class MealIngredientsRepository : RepositoryBase<MealIngredients>, IMealIngredientsRepository
{
    private readonly IPgsqlDbContext _readDbContext;
    public MealIngredientsRepository(MealsDbContext context, IPgsqlDbContext readDbContext) : base(context)
    {
        _readDbContext = readDbContext;
    }
}