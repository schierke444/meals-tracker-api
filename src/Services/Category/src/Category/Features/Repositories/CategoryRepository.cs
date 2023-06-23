using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.EFCore;
using Category.Commons.Interfaces;
using Category.Features.Dtos;
using Category.Persistence;

namespace Category.Features.Repositories;

public class CategoryRepository : RepositoryBase<Entities.Category>, ICategoryRepository
{
    private readonly IPgsqlDbContext _readDbContext;
    public CategoryRepository(CategoryDbContext context, IPgsqlDbContext readDbContext) : base(context)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PaginatedResults<CategoryDetailsDto>> GetPagedCategoryList(string? search, string? sortColumn, string? sortOrder, int page = 1, int pageSize = 10)
    {
        // Query for Fetching all the Categories
        var sql = @"SELECT Id, Name, created_at CreatedAt, updated_at UpdatedAt
                    FROM category";

        // Query for Total Items Count
        var totalItemsSql = "SELECT COUNT(id) FROM category";

        // Will Assign filters for both totalItemsSql and Sql 
        if(!string.IsNullOrEmpty(search)) {
            var where = $" WHERE name LIKE '%{search}%' ";
            sql += where;
            totalItemsSql += where;
        }

        // Apply Sorting
        sql += sortOrder == "desc" ? $" ORDER BY {GetColumn(sortColumn)} DESC" : $" ORDER BY {GetColumn(sortColumn)}";

        // Get Total Items with/without Filter, and create Page Metadata instance.
        var totalItems = await _readDbContext.ExecuteScalarAsync<int>(totalItemsSql);
        var pageData = new PageMetadata(page, pageSize, totalItems);

        // Apply Limit and Skip Filter
        sql += $" LIMIT {pageSize} OFFSET {pageSize * (page - 1)}";

        var results = await _readDbContext.QueryAsync<CategoryDetailsDto>(sql);
        PaginatedResults<CategoryDetailsDto> paginated = new(results, pageData);

        return paginated;
    }

    public async Task<CategoryDetailsDto> GetCategoryById(string categoryId)
    {
        var sql = "SELECT * FROM Category Where Id::text = @Id";
        var result = await _readDbContext.QueryFirstOrDefaultAsync<CategoryDetailsDto>(sql, new { Id = categoryId});

        return result;
    }
    
    private static string GetColumn(string? sortColumn)
    {
        var s = sortColumn?.ToLower() switch {
            "content" => $"content",
            "created_at" => $"created_at",
            "updated_at" => $"updated_at",
            _ => $"p.Id"
        };

        return s;
    } 
}