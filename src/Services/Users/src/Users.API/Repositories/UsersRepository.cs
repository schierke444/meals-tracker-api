using BuildingBlocks.EFCore;
using Users.API.Entities;
using Users.API.Persistence;

namespace Users.API.Repositories;

public class UsersRepository : RepositoryBase<User>, IUsersRepository
{
    public UsersRepository(ApplicationDbContext context) : base(context)
    {
    }
}