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

    public async Task<IEnumerable<MealsDto>> GetAllMealsByOwnerId(string OwnerId, int page = 1, int pageSize =10)
    {
        var sql = @"SELECT m.Id, m.Meal_Name MealName, m.Meal_Review MealReview, m.Rating, m.Created_At CreatedAt, u.Id, u.Username
                    FROM Meals m
                    INNER JOIN Users u ON u.Id = m.Owner_Id
                    WHERE Owner_Id::text = @OwnerId 
                    ORDER BY CreatedAt DESC " + 
                    $"LIMIT {pageSize} OFFSET {pageSize * (page - 1)}";

        var results = await _readDbContext.QueryMapAsync<MealsDto, UserDetailsDto, MealsDto>(
            sql, 
            (meals, users) => {
                meals.Owner = users;
                return meals;
            },
            new { OwnerId },
            splitOn: "Id");

        return results;
    }

    public async Task<int> GetMealsCountByOwnerId(string OwnerId)
    {
        var sql = "SELECT COUNT(*) FROM Meals WHERE Owner_Id::text = @OwnerId";

        var results = await _readDbContext.ExecuteScalarAsync<int>(sql, new {OwnerId });

        return results;
    }
}