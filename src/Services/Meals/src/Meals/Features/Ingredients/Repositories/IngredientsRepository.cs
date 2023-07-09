using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.EFCore;
using Dapper;
using Meals.Entities;
using Meals.Features.Ingredients.Dtos;
using Meals.Features.Ingredients.Interfaces;
using Meals.Features.Meals.Dtos;
using Meals.Persistence;

namespace Meals.Features.Ingredients.Repositories;

public sealed class IngredientsRepository : RepositoryBase<Ingredient>, IIngredientsRepository
{
    private readonly MealsDbContext _mealsContext;
    private readonly IPgsqlDbContext _readDbContext;
    public IngredientsRepository(MealsDbContext context, IPgsqlDbContext readDbContext) : base(context)
    {
        _mealsContext = context;
        _readDbContext = readDbContext;
    }

    public async Task<PaginatedResults<IngredientsDto>> GetPagedIngredientList(
        string? search,
        string? sortColumn,
        string? sortOrder,
        int page = 1,
        int pageSize = 10
    )
    {
        var sql = @"SELECT Id, Name
                    FROM Ingredients";

        var totalItemsSql = @"SELECT COUNT(Id) 
                            FROM Ingredients";

        if(!string.IsNullOrEmpty(search))
        {
            var where = $" WHERE Name LIKE '%{search}%' ";
            sql += where;
            totalItemsSql += where;
        }

        sql += sortOrder == "desc" ? $" ORDER BY {GetColumn(sortColumn)} DESC" : $" ORDER BY {GetColumn(sortColumn)}";

        var totalItems = await _readDbContext.ExecuteScalarAsync<int>(totalItemsSql);
        var pageData = new PageMetadata(page, pageSize, totalItems);
        
        sql += $" LIMIT {pageSize}";
        sql += $" OFFSET {pageSize * (page - 1)}";        

        var results = await _readDbContext.QueryAsync<IngredientsDto>(sql);

        PaginatedResults<IngredientsDto> paginated = new(results, pageData);
        return paginated;
    }

    public async Task<PaginatedResults<IngredientsDto>> GetPagedMealsIngredientsByMealId(
        string MealId, 
        string? search, 
        string? sortColumn, 
        string? sortOrder, 
        int page = 1, 
        int pageSize = 10)
    {
        var sql = @"SELECT i.id, i.name FROM meal_ingredients mi
                    INNER JOIN Ingredients i ON mi.ingredient_id = i.id
                    WHERE mi.meal_id::text = @MealId"; 

        var totalItemsSql = @"SELECT COUNT(mi.id) 
                            FROM meal_ingredients mi
                            INNER JOIN Ingredients i ON mi.ingredient_id = i.id
                            WHERE mi.meal_id::text = @MealId";

        if(!string.IsNullOrEmpty(search)) 
        {
            var where = $" AND i.name LIKE '%{search}%' ";
            sql += where;
            totalItemsSql += where;
        }

        sql += sortOrder == "desc" ? $" ORDER BY {GetColumn(sortColumn)} DESC" : $" ORDER BY {GetColumn(sortColumn)}";

        var totalItems = await _readDbContext.ExecuteScalarAsync<int>(totalItemsSql, param: new {MealId});
        var pageData = new PageMetadata(page, pageSize, totalItems);
        
        sql += $" LIMIT {pageSize}";
        sql += $" OFFSET {pageSize * (page - 1)}";

        var results = await _readDbContext.QueryAsync<IngredientsDto>(sql, new {MealId});

        PaginatedResults<IngredientsDto> paginated = new(results, pageData);

        return paginated;
    }

    public bool VerifyIngredientsByIds(IEnumerable<AddIngredientsToMealsDto> IngredientIds)
    {
        // check if there's a duplicate in the ingredient ids
        HashSet<Guid> hash = new();
        foreach (var item in IngredientIds)
        {
            if(hash.Contains(item.Id)) return false;

            hash.Add(item.Id);
        } 

        // verify the ingredient ids if exists in the database
        var result = hash 
            .All(ingredientId => _mealsContext.Ingredients.Select(x => x.Id).Contains(ingredientId));

        return result;
    }

    private static string GetColumn(string? sortColumn)
    {
        var s = sortColumn?.ToLower() switch {
            "created_at" => "created_at",
            "updated_at" => "updated_at",
            _ => $"name"
        };

        return s;
    }
}