using Microsoft.EntityFrameworkCore;
using Users.API.Entities;

namespace Users.API.Persistence;

public interface IApplicationDbContext
{
    DbSet<User> Users {get;}
}