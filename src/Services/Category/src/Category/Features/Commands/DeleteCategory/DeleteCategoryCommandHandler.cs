using BuildingBlocks.Commons.Exceptions;
using Category.Commons.Interfaces;
using MediatR;

namespace Category.Features.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {

        var result = await _categoryRepository.GetValue(x => x.Id.ToString() == request.CategoryId, false) 
            ?? throw new NotFoundException($"Category with Id '{request.CategoryId}' was not found.");

        _categoryRepository.Delete(result);
        await _categoryRepository.SaveChangesAsync(cancellationToken);
    }
}