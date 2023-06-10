using Auth.Commons.Dtos;
using Auth.Entities;
using Auth.Persistence;
using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.EFCore;

namespace Auth.Repositories;

public class AuthRepository : RepositoryBase<User>, IAuthRepository
{
    private readonly IPgsqlDbContext _readDbContext;
    public AuthRepository(AuthDbContext context , IPgsqlDbContext readDbContext) : base(context)
    {
        _readDbContext = readDbContext;
    }

    public async Task<UserDetailsDto?> GetUserByUsername(string username)
    {
        var sql = "SELECT Id, Username, Password From Users WHERE Username = @Username";
        var result = await _readDbContext.QueryFirstOrDefaultAsync<UserDetailsDto>(sql, new { Username = username});

        return result;
    }

    public async Task<UserDetailsDto?> GetUserById(string UserId)
    {
        var sql = "SELECT Id, Username, Password From Users WHERE Id::text = @Id";
        var result = await _readDbContext.QueryFirstOrDefaultAsync<UserDetailsDto>(sql, new { Id = UserId});

        return result;
    }
}