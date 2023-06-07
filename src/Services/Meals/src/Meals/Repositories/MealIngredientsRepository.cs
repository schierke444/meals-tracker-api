using BuildingBlocks.EFCore;
using Meals.Commons.Interfaces;
using Meals.Entities;
using Meals.Persistence;

namespace Meals.Repositories;

public sealed class MealIngredientsRepository : RepositoryBase<MealIngredients>, IMealIngredientsRepository
{
    public MealIngredientsRepository(MealsDbContext context) : base(context)
    {
    }
}