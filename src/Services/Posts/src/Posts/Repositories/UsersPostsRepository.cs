using BuildingBlocks.Commons.Interfaces;
using Posts.Commons.Interfaces;
using Posts.Entities;
using Posts.Persistence;

namespace Posts.Repositories;

public class UsersPostsRepository : IUsersPostsRepository
{
    private readonly PostsDbContext _context;
    private readonly IPgsqlDbContext _readDbContext;
    public UsersPostsRepository(PostsDbContext context, IPgsqlDbContext readDbContext)
    {
        _context = context;
        _readDbContext = readDbContext;
    }

    

    public async Task<UsersPosts?> GetUserById(Guid Id)
    {
        var sql = "SELECT user_id UserId, username Username FROM users_posts WHERE user_id::text = @Id";

        var results = await _readDbContext.QueryFirstOrDefaultAsync<UsersPosts>(sql, new {Id = Id.ToString()});

        return results;
    }
    public async Task Add(UsersPosts entity)
    {
        await _context.UsersPosts.AddAsync(entity);
    }
    public void Remove(UsersPosts entity)
    {
        _context.UsersPosts.Remove(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}