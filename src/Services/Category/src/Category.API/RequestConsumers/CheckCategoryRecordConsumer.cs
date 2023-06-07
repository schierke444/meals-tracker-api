using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events;
using Category.Commons.Interfaces;
using MassTransit;

namespace Category.API.RequestConsumers;

public class CheckCategoryRecordConsumer : IConsumer<CheckCategoryRecord>
{
    private readonly ICategoryRepository _categoryRepository;
    public CheckCategoryRecordConsumer(ICategoryRepository categoryRepository) 
    {
        _categoryRepository = categoryRepository; 
    }
    public async Task Consume(ConsumeContext<CheckCategoryRecord> context)
    {
        var category = await _categoryRepository.GetValue(
            x => x.Id.ToString() == context.Message.CategoryId, 
            x => new CategoryRecordResult { Id = x.Id, Name = x.Name, CreatedAt = x.CreatedAt, UpdatedAt = x.UpdatedAt}
            )
            ?? throw new NotFoundException($"Category with Id '{context.Message.CategoryId}' was not found.");

        await context.RespondAsync(category);
    }
}
