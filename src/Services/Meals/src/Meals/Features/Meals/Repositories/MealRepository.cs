using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.EFCore;
using Meals.Commons.Interfaces;
using Meals.Entities;
using Meals.Features.Meals.Dtos;
using Meals.Persistence;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Meals.Features.Ingredients.Dtos;
using Category.Features.Dtos;

namespace Meals.Repositories;

public sealed class MealsRepository : RepositoryBase<Meal>, IMealsRepository
{
    private readonly IPgsqlDbContext _readDbContext;
    private readonly MealsDbContext _mealContext;
    public MealsRepository(MealsDbContext context, IPgsqlDbContext readDbContext) : base(context)
    {
       _mealContext = context; 
        _readDbContext = readDbContext;
    }

    public async Task<IEnumerable<MealsDto>> GetAllMeals()
    {
        var sql = "SELECT Id, Meal_Name, CreatedAt FROM Meals";
        var results = await _readDbContext.QueryAsync<MealsDto>(sql);

        return results;
    }

    public async Task<MealDetailsDto?> GetMealsById(string MealId, bool includeIngredients = true, bool includeCategory = true)
    {
        IQueryable<Meal> query = _mealContext.Meals
            .AsNoTracking();

        if(includeIngredients)
            query = query
                .Include(x => x.MealIngredient)
                .ThenInclude(x => x.Ingredients);

        if(includeCategory)
            query = query
                .Include(x => x.MealCategories)
                .ThenInclude(x => x.Category);

        return await query
            .Include(x => x.UsersMeals)
            .Where(x => x.Id.ToString() == MealId)
            .Select(x => new MealDetailsDto(
                x.Id,
                x.MealName,
                x.MealReview,
                x.Rating,
                x.Instructions,
                x.MealIngredient.Select(x => new IngredientsWithAmountDto(x.IngredientId, x.Ingredients!.Name, x.Amount)),
                x.MealCategories.Select(x => new CategoryDto(x.CategoryId, x.Category!.Name)),
                new UserDetailsDto(x.OwnerId, x.UsersMeals!.Username),
                x.CreatedAt
            )).FirstOrDefaultAsync();        
    }

    public async Task<PaginatedResults<MealsDto>> GetPagedMealsListByOwnerId(
        string OwnerId, 
        string? search,
        string? sortColumn,
        string? sortOrder,
        int page = 1, 
        int pageSize =10)
    {
        var sql = @"SELECT m.Id, m.Meal_Name MealName, m.Rating, m.Created_At CreatedAt
                    FROM Meals m
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

        var results = await _readDbContext._pgConnection.QueryAsync<MealsDto>(sql, param: new {OwnerId});

        PaginatedResults<MealsDto> paginated = new(results, pageData);

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