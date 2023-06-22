using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using Category.Features.Dtos;

namespace Category.Commons.Interfaces;

public interface ICategoryRepository : IWriteRepository<Entities.Category>, IReadRepository<Entities.Category>
{
    Task<PaginatedResults<CategoryDetailsDto>> GetPagedCategoryList(
        string? search,
        string? sortColumn,
        string? sortOrder,
        int page = 1,
        int pageSize = 10
    );

    Task<CategoryDetailsDto> GetCategoryById(string categoryId);
}