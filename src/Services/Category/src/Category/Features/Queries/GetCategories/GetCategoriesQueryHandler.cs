using BuildingBlocks.Commons.CQRS;
using Category.Commons.Interfaces;
using Category.Features.Dtos;

namespace Category.Features.Queries.GetCategories;

sealed class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetAllCategories();

        return results;
    }
}