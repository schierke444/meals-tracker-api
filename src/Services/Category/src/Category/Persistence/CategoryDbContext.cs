using BuildingBlocks.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Category.Persistence; 

public class CategoryDbContext : ApplicationDbContextBase
{
    public DbSet<Entities.Category> Categories => Set<Entities.Category>();
    public CategoryDbContext(IConfiguration config) : base(config)
    {
    }

    public CategoryDbContext(DbContextOptions options, IConfiguration config) : base(options, config)
    {
    }
}