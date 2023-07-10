using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Category.Features.Dtos;

namespace Meals.Features.Category.Queries.GetCategories.v1;

sealed class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, PaginatedResults<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<PaginatedResults<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetPagedCategoryList(
            request.Search,
            request.SortColumn,
            request.SortOrder,
            request.Page,
            request.PageSize
        );

        return results;
    }
}