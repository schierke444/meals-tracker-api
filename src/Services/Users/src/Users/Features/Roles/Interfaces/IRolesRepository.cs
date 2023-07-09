using BuildingBlocks.Commons.Interfaces;

namespace Users.Features.Roles.Interfaces;

public interface IRolesRepository : IWriteRepository<Entities.Roles>, IReadRepository<Entities.Roles>
{

}