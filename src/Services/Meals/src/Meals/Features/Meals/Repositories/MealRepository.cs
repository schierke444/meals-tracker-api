using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.EFCore;
using Meals.Commons.Interfaces;
using Meals.Entities;
using Meals.Features.Meals.Dtos;
using Meals.Persistence;

namespace Meals.Repositories;

public sealed class MealsRepository : RepositoryBase<Meal>, IMealsRepository
{
    private readonly IPgsqlDbContext _readDbContext;
    public MealsRepository(MealsDbContext context, IPgsqlDbContext readDbContext) : base(context)
    {
        _readDbContext = readDbContext;
    }

    public async Task<IEnumerable<MealsDto>> GetAllMeals()
    {
        var sql = "SELECT Id, Meal_Name, CreatedAt FROM Meals";
        var results = await _readDbContext.QueryAsync<MealsDto>(sql);

        return results;
    }

    public async Task<MealDetailsDto> GetMealsById(string MealId)
    {
        var sql = "SELECT * FROM Meals WHERE Id::text = @Id";
        var results = await _readDbContext.QueryFirstOrDefaultAsync<MealDetailsDto>(sql, new {Id = MealId});

        return results;
    }

    public async Task<IEnumerable<MealsDto>> GetAllMealsByOwnerId(string OwnerId)
    {
        var sql = "SELECT Id, Meal_Name, Meal_Review, Rating, Created_At FROM Meals WHERE Owner_Id::text = @OwnerId";
        var results = await _readDbContext.QueryAsync<MealsDto>(sql, new {OwnerId = OwnerId});

        return results;
    }
}