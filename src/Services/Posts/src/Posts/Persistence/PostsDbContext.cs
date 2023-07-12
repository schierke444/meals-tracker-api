using System.Reflection;
using BuildingBlocks.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Posts.Entities;

namespace Posts.Persistence;

public class PostsDbContext : ApplicationDbContextBase
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<LikedPosts> LikedPosts => Set<LikedPosts>();
    public DbSet<UsersPosts> UsersPosts => Set<UsersPosts>();
    public PostsDbContext(IConfiguration config) : base(config)
    {
    }

    public PostsDbContext(DbContextOptions options, IConfiguration config) : base(options, config)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
