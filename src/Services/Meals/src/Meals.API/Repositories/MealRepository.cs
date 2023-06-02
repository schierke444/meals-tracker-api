using BuildingBlocks.EFCore;
using Meals.API.Entities;
using Meals.API.Persistence;

namespace Meals.API.Repositories;

public sealed class MealsRepository : RepositoryBase<Meal>, IMealsRepository
{
    public MealsRepository(ApplicationDbContext context) : base(context)
    {
    }
}