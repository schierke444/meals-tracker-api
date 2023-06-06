using Auth.Entities;
using Auth.Persistence;
using BuildingBlocks.EFCore;

namespace Auth.Repositories;

public class AuthRepository : RepositoryBase<User>, IAuthRepository
{
    public AuthRepository(AuthDbContext context) : base(context)
    {
    }
}