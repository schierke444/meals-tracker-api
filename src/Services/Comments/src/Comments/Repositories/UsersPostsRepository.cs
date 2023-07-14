using BuildingBlocks.Commons.Interfaces;
using Comments.Commons.Interfaces;
using Comments.Entities;
using Comments.Persistence;

namespace Posts.Repositories;

public class UsersCommentsRepository : IUsersCommentsRepository
{
    private readonly CommentsDbContext _context;
    private readonly IPgsqlDbContext _readDbContext;
    public UsersCommentsRepository(CommentsDbContext context, IPgsqlDbContext readDbContext)
    {
        _context = context;
        _readDbContext = readDbContext;
    }

    public async Task<UsersComments?> GetUserById(string Id)
    {
        var sql = "SELECT user_id UserId, username Username FROM users_comments WHERE user_id::text = @Id";

        var results = await _readDbContext.QueryFirstOrDefaultAsync<UsersComments>(sql, new {Id});

        return results;
    }
    public async Task Add(UsersComments entity)
    {
        await _context.UsersComments.AddAsync(entity);
    }
    public void Remove(UsersComments entity)
    {
        _context.UsersComments.Remove(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}