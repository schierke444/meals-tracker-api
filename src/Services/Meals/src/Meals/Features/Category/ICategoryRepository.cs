using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using Category.Features.Dtos;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Category;

public interface ICategoryRepository : IWriteRepository<Entities.Category>, IReadRepository<Entities.Category>
{
    Task<PaginatedResults<CategoryDto>> GetPagedCategoryList(
        string? search,
        string? sortColumn,
        string? sortOrder,
        int page = 1,
        int pageSize = 10
    );
    bool VerifyCategoryByIds(IEnumerable<AddCategoryToMealsDto> CategoryIds);
}