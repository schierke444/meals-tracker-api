using BuildingBlocks.Commons.Interfaces;
using Category.Features.Dtos;

namespace Category.Commons.Interfaces;

public interface ICategoryRepository : IWriteRepository<Entities.Category>, IReadRepository<Entities.Category>
{
    Task<IEnumerable<CategoryDto>> GetAllCategories();

    Task<CategoryDetailsDto> GetCategoryById(string categoryId);
}