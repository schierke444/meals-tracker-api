using BuildingBlocks.EFCore;
using Category.Commons.Interfaces;
using Category.Persistence;

namespace Category.Repositories;

public class CategoryRepository : RepositoryBase<Entities.Category>, ICategoryRepository
{
    public CategoryRepository(CategoryDbContext context) : base(context)
    {
    }
}