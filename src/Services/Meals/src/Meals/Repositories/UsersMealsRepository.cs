using BuildingBlocks.Commons.Interfaces;
using Meals.Commons.Interfaces;
using Meals.Entities;
using Meals.Persistence;

namespace Posts.Repositories;

public class UsersMealsRepository : IUsersMealsRepository 
{
    private readonly MealsDbContext _context;
    private readonly IPgsqlDbContext _readDbContext;
    public UsersMealsRepository(MealsDbContext context, IPgsqlDbContext readDbContext)
    {
        _context = context;
        _readDbContext = readDbContext;
    }

    

    public async Task<UsersMeals?> GetUserById(Guid Id)
    {
        var sql = "SELECT user_id UserId, username Username FROM users_meals WHERE user_id::text = @Id";

        var results = await _readDbContext.QueryFirstOrDefaultAsync<UsersMeals>(sql, new {Id = Id.ToString()});

        return results;
    }
    public async Task Add(UsersMeals entity)
    {
        await _context.UsersMeals.AddAsync(entity);
    }
    public void Remove(UsersMeals entity)
    {
        _context.UsersMeals.Remove(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}