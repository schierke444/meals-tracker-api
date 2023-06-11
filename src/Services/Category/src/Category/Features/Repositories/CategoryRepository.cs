using BuildingBlocks.Commons.Interfaces;
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

    public async Task<IEnumerable<CategoryDto>> GetAllCategories()
    {
        var sql = "SELECT Id, Name FROM Category";
        var results = await _readDbContext.QueryAsync<CategoryDto>(sql);

        return results;
    }

    public async Task<CategoryDetailsDto> GetCategoryById(string categoryId)
    {
        var sql = "SELECT * FROM Category Where Id::text = @Id";
        var result = await _readDbContext.QueryFirstOrDefaultAsync<CategoryDetailsDto>(sql, new { Id = categoryId});

        return result;
    }
}