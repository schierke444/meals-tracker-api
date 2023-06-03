using BuildingBlocks.EFCore;
using Ingredients.API.Entities;
using Ingredients.API.Persistence;

namespace Ingredients.API.Repositories;

public sealed class IngredientsRepository : RepositoryBase<Ingredient>, IIngredientsRepository
{
    public IngredientsRepository(ApplicationDbContext context) : base(context)
    {
    }
}