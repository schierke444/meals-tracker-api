using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Category.Commons.Interfaces;
using Category.Features.Dtos;

namespace Category.Features.Queries.GetCategories;

sealed class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, PaginatedResults<CategoryDetailsDto>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<PaginatedResults<CategoryDetailsDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetPagedCategoryList(
            request.Search,
            request.SortColumn,
            request.SortOrder,
            request.Page,
            request.PageSize
        );

        // var totalItems = await _categoryRepository.GetTotalCategoryCount();
        // var pageData = new PageMetadata(request.Page, request.PageSize, totalItems);

        // var paginated = new PaginatedResults<CategoryDetailsDto>(results, pageData);

        return results;
    }
}