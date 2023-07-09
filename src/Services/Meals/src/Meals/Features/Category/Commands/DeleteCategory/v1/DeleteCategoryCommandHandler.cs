using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using MediatR;

namespace Meals.Features.Category.Commands.DeleteCategory.v1;

public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand, Unit>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {

        var result = await _categoryRepository.GetValue(x => x.Id.ToString() == request.CategoryId, false) 
            ?? throw new NotFoundException($"Category with Id '{request.CategoryId}' was not found.");

        _categoryRepository.Delete(result);
        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}