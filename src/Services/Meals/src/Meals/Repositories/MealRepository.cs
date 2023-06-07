using BuildingBlocks.EFCore;
using Meals.Commons.Interfaces;
using Meals.Entities;
using Meals.Persistence;

namespace Meals.Repositories;

public sealed class MealsRepository : RepositoryBase<Meal>, IMealsRepository
{
    public MealsRepository(MealsDbContext context) : base(context)
    {
    }
}