using BuildingBlocks.Commons.CQRS;
using Category.Commons.Interfaces;

namespace Category.Features.Commands.CreateCategory;

public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Entities.Category newCategory = new()
        {
            Name = request.Name
        };

        await _categoryRepository.Add(newCategory);
        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return newCategory.Id;
    }
}
