using Auth.API.Entity;
using Auth.API.Persistence;
using Auth.API.Repositories;
using BuildingBlocks.EFCore;

namespace Auth.API.Services;

public class AuthRepository : RepositoryBase<User>, IAuthRepository
{
    public AuthRepository(ApplicationDbContext context) : base(context)
    {
    }
}