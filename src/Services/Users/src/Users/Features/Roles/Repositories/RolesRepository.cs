using BuildingBlocks.EFCore;
using Users.Features.Roles.Interfaces;
using Users.Persistence;

namespace Users.Features.Roles.Repositories;

public sealed class RolesRepository : RepositoryBase<Entities.Roles>, IRolesRepository
{
    public RolesRepository(ApplicationDbContext context) : base(context)
    {
    }
}