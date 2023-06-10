using Auth.Commons.Dtos;
using Auth.Entities;
using BuildingBlocks.Commons.Interfaces;

namespace Auth.Repositories;

public interface IAuthRepository : IReadRepository<User>, IWriteRepository<User>
{
    Task<UserDetailsDto?> GetUserByUsername(string username);
    Task<UserDetailsDto?> GetUserById(string UserId);
}