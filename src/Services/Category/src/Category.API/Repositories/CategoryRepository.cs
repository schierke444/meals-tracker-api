using BuildingBlocks.EFCore;
using Category.API.Persistence;

namespace Category.API.Repositories;

public class CategoryRepository : RepositoryBase<Entities.Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}