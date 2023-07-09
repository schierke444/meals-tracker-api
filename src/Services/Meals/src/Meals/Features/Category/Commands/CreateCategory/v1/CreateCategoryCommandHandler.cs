using BuildingBlocks.Commons.CQRS;

namespace Meals.Features.Category.Commands.CreateCategory.v1;

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
