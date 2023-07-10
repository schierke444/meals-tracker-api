using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.EFCore;
using Category.Features.Dtos;
using Meals.Features.Meals.Dtos;
using Meals.Persistence;

namespace Meals.Features.Category.Repositories;

public class CategoryRepository : RepositoryBase<Entities.Category>, ICategoryRepository
{
    private readonly IPgsqlDbContext _readDbContext;
    private readonly MealsDbContext _mealsContext;
    public CategoryRepository(IPgsqlDbContext readDbContext, MealsDbContext mealsContext) : base(mealsContext)
    {
        _readDbContext = readDbContext;
        _mealsContext = mealsContext;
    }

    public async Task<PaginatedResults<CategoryDto>> GetPagedCategoryList(string? search, string? sortColumn, string? sortOrder, int page = 1, int pageSize = 10)
    {
        // Query for Fetching all the Categories
        var sql = @"SELECT Id, Name
                    FROM category ";

        // Query for Total Items Count
        var totalItemsSql = "SELECT COUNT(id) FROM category";

        // Will Assign filters for both totalItemsSql and Sql 
        if(!string.IsNullOrEmpty(search)) {
            var where = $" WHERE name LIKE '%{search.ToLower()}%' ";
            sql += where;
            totalItemsSql += where;
        }

        // Apply Sorting
        sql += sortOrder == "desc" ? $" ORDER BY {GetColumn(sortColumn)} DESC" : $" ORDER BY {GetColumn(sortColumn)} ASC";

        // Get Total Items with/without Filter, and create Page Metadata instance.
        var totalItems = await _readDbContext.ExecuteScalarAsync<int>(totalItemsSql);
        var pageData = new PageMetadata(page, pageSize, totalItems);

        // Apply Limit and Skip Filter
        sql += $" LIMIT {pageSize} OFFSET {pageSize * (page - 1)}";

        var results = await _readDbContext.QueryAsync<CategoryDto>(sql);
        PaginatedResults<CategoryDto> paginated = new(results, pageData);

        return paginated;
    }

    private static string GetColumn(string? sortColumn)
    {
        var s = sortColumn?.ToLower() switch {
            "created_at" => $"created_at",
            "updated_at" => $"updated_at",
            _ => $"name"
        };

        return s;
    }

    public bool VerifyCategoryByIds(IEnumerable<AddCategoryToMealsDto> CategoryIds)
    {
        // check if there's a duplicate in the category ids
        HashSet<Guid> hash = new();
        foreach (var item in CategoryIds)
        {
            if(hash.Contains(item.Id)) return false;

            hash.Add(item.Id);
        } 

        // verify the category ids if exists in the database
        var result = hash 
            .All(categoryId => _mealsContext.Categories.Select(x => x.Id).Contains(categoryId));

        return result;
    }
}