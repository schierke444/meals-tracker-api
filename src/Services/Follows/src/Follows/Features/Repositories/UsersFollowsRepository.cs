using BuildingBlocks.Commons.Interfaces;
using Follows.Entities;
using Follows.Features.Interfaces;
using Follows.Persistence;

namespace Follows.Features.Repositories;

public sealed class UsersFollowsRepository : IUsersFollowsRepository
{
    private readonly FollowDbContext _context;
    private readonly IPgsqlDbContext _readDbContext;
    public UsersFollowsRepository(FollowDbContext context, IPgsqlDbContext readDbContext)
    {
        _context = context;
        _readDbContext = readDbContext;
    }
    public async Task Add(UsersFollows entity)
    {
        await _context.UsersFollows.AddAsync(entity);
    }

    public async Task<UsersFollows?> GetUserById(Guid Id)
    {
        var sql = "SELECT user_id UserId, username Username FROM users_follows WHERE user_id::text = @Id";

        var results = await _readDbContext.QueryFirstOrDefaultAsync<UsersFollows>(sql, new {Id = Id.ToString()});

        return results;
    }

    public void Remove(UsersFollows entity)
    {
        _context.Remove(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}