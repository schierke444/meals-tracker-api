using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.EFCore;
using Meals.Entities;
using Meals.Features.Meals.Interfaces;
using Meals.Persistence;

namespace Meals.Features.Meals.Repositories;

public sealed class MealCategoryRepository : RepositoryBase<MealCategory>, IMealCategoryRepository
{
    private readonly IPgsqlDbContext _readDbContext;
    public MealCategoryRepository(MealsDbContext context, IPgsqlDbContext readDbContext) : base(context)
    {
        _readDbContext = readDbContext;
    }
}