using BuildingBlocks.Commons.Interfaces;
using Users.Entities;

namespace Users.Features.Users.Interfaces;

public interface IUsersRepository : IWriteRepository<User>, IReadRepository<User>
{
}