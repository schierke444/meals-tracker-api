using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.EFCore;
using Meals.Entities;
using Meals.Features.Ingredients.Dtos;
using Meals.Features.Ingredients.Interfaces;
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

    public async Task<PaginatedResults<IngredientDetailsDto>> GetPagedIngredientList(
        string? search,
        string? sortColumn,
        string? sortOrder,
        int page = 1,
        int pageSize = 10
    )
    {
        var sql = @"SELECT Id, Name, created_at CreatedAt, updated_at UpdatedAt
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

        var results = await _readDbContext.QueryAsync<IngredientDetailsDto>(sql);

        PaginatedResults<IngredientDetailsDto> paginated = new(results, pageData);
        return paginated;
    }

    public async Task<IngredientDetailsDto> GetIngredientById(string ingredientId)
    {
        var sql = "SELECT * From Ingredients Where Id = @Id";
        var results = await _readDbContext.QueryFirstOrDefaultAsync<IngredientDetailsDto>(sql, new {Id = ingredientId});

        return results;
    }

    public bool VerifyIngredientsByIds(IEnumerable<Guid> IngredientIds)
    {
        // check if there's a duplicate in the ingredient ids
        HashSet<Guid> hash = new();
        foreach (var item in IngredientIds)
        {
            if(hash.Contains(item)) return false;

            hash.Add(item);
        } 

        // verify the ingredient ids if exists in the database
        var result = hash 
            .All(ingredientId => _mealsContext.Ingredients.Select(x => x.Id).Contains(ingredientId));

        return result;
    }
    private static string GetColumn(string? sortColumn)
    {
        var s = sortColumn?.ToLower() switch {
            "content" => "content",
            "name" => "name",
            "created_at" => "created_at",
            "updated_at" => "updated_at",
            _ => $"Id"
        };

        return s;
    }

}