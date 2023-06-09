using BuildingBlocks.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Posts.Persistence;

public class PostsDbContext : ApplicationDbContextBase
{
    private readonly IConfiguration _config;
    public PostsDbContext(IConfiguration config) : base(config)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_config["ConnectionStrings:DB"]);
        base.OnConfiguring(optionsBuilder);
    }
}
