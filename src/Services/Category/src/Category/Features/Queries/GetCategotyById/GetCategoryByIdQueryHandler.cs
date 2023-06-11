using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using Category.Commons.Interfaces;
using Category.Features.Dtos;

namespace Category.Features.Queries.GetCategotyById;

sealed class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDetailsDto>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDetailsDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.GetValue(
            x => x.Id.ToString() == request.CategoryId,
            x => new CategoryDetailsDto(x.Id, x.Name, x.CreatedAt, x.UpdatedAt)
        )
            ?? throw new NotFoundException($"Category with Id '{request.CategoryId}' was not found.");

       return result; 
    }
}