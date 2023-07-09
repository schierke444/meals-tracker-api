using BuildingBlocks.EFCore;
using Users.Entities;
using Users.Features.Users.Interfaces;
using Users.Persistence;

namespace Users.Features.Users.Repositories;

public class UsersRepository : RepositoryBase<User>, IUsersRepository
{
    public UsersRepository(ApplicationDbContext context) : base(context)
    {
    } 
}