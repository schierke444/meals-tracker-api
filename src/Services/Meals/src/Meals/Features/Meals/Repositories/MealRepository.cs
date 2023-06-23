using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.EFCore;
using Meals.Commons.Interfaces;
using Meals.Entities;
using Meals.Features.Meals.Dtos;
using Meals.Persistence;
using Dapper;

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

    public async Task<PaginatedResults<MealDetailsDto>> GetPagedMealsListByOwnerId(
        string OwnerId, 
        string? search,
        string? sortColumn,
        string? sortOrder,
        int page = 1, 
        int pageSize =10)
    {
        var sql = @"SELECT m.Id, m.Meal_Name MealName, m.Meal_Review MealReview, m.Rating, m.Created_At CreatedAt, c.Id, c.Name Category, u.Id, u.Username
                    FROM Meals m
                    INNER JOIN Users u ON u.Id = m.Owner_Id
                    INNER JOIN Category c ON c.Id = m.Category_Id
                    WHERE Owner_Id::text = @OwnerId";

        var totalItemsSql = "SELECT COUNT(id) FROM Meals WHERE Owner_Id::text = @OwnerId";

        if(!string.IsNullOrEmpty(search)) 
        {
            var where = $" AND meal_name LIKE '%{search}%' ";
            sql += where;
            totalItemsSql += where;
        }

        sql += sortOrder == "desc" ? $" ORDER BY {GetMealsColumn(sortColumn)} DESC" : $" ORDER BY {GetMealsColumn(sortColumn)}";

        var totalItems = await _readDbContext.ExecuteScalarAsync<int>(totalItemsSql, param: new {OwnerId});
        var pageData = new PageMetadata(page, pageSize, totalItems);
        
        sql += $" LIMIT {pageSize}";
        sql += $" OFFSET {pageSize * (page - 1)}";

        var results = await _readDbContext._pgConnection.QueryAsync<MealDetailsDto, CategoryDetailsDto, UserDetailsDto, MealDetailsDto>(
            sql, 
            (meals, category, users) => {
                meals.Category = category;
                meals.Owner = users;
                return meals;
            },
            new { OwnerId },
            splitOn: "Id");

        PaginatedResults<MealDetailsDto> paginated = new(results, pageData);

        return paginated;
    }

    public async Task<int> GetMealsCountByOwnerId(string OwnerId, string? query = null)
    {
        var sql = "SELECT COUNT(id) FROM Meals WHERE Owner_Id::text = @OwnerId";

        var results = await _readDbContext.ExecuteScalarAsync<int>(sql, new {OwnerId });

        return results;
    }
    private static string GetMealsColumn(string? sortColumn)
    {
        var s = sortColumn?.ToLower() switch {
            "meal_name" => $"MealName",
            "created_at" => $"CreatedAt",
            "updated_at" => $"UpdatedAt",
            _ => $"m.Id"
        };

        return s;
    }

    private static string GetMealIngredientColumn(string? sortColumn)
    {
        var s = sortColumn?.ToLower() switch {
            "name" => $"name",
            "created_at" => $"CreatedAt",
            "updated_at" => $"UpdatedAt",
            _ => $"m.Id"
        };

        return s;
    }
}