using BuildingBlocks.EFCore;
using Comments.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Comments.Persistence;
public sealed class CommentsDbContext : ApplicationDbContextBase
{
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<UsersComments> UsersComments => Set<UsersComments>();

    public CommentsDbContext(IConfiguration config) : base(config)
    {
    }

    public CommentsDbContext(DbContextOptions opt, IConfiguration config) : base(opt, config)
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
