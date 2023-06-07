using BuildingBlocks.Commons.CQRS;
using Category.Commons.Dtos;
using Category.Commons.Interfaces;

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
        var results = await _categoryRepository.GetAllValues(
            x => new CategoryDto(x.Id, x.Name),
            null,
            true 
        );

        return results;
    }
}