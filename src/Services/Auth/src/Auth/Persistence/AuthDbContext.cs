using Auth.Entities;
using BuildingBlocks.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Auth.Persistence;
public class AuthDbContext : ApplicationDbContextBase
{
    public DbSet<User> Users => Set<User>();

    public AuthDbContext(IConfiguration Config) : base(Config)
    {
    }
}