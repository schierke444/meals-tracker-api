using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events;
using Category.API.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Category.API.RequestConsumers;

public class CheckCategoryRecordConsumer : IConsumer<CheckCategoryRecord>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CheckCategoryRecordConsumer> _logger;
    public CheckCategoryRecordConsumer(ICategoryRepository categoryRepository, ILogger<CheckCategoryRecordConsumer> logger)
    {
        _categoryRepository = categoryRepository; 
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<CheckCategoryRecord> context)
    {
        _logger.LogInformation(context.Message.CategoryId);
        // var category = await _context.Categories
        //     .Where(x => x.Id.ToString() == context.Message.CategoryId)
        //     .Select(x => new CategoryRecordResult { Id = x.Id, Name = x.Name, CreatedAt = x.CreatedAt, UpdatedAt = x.UpdatedAt})
        //     .FirstOrDefaultAsync()
        var category = await _categoryRepository.GetValue(
            x => x.Id.ToString() == context.Message.CategoryId, 
            x => new CategoryRecordResult { Id = x.Id, Name = x.Name, CreatedAt = x.CreatedAt, UpdatedAt = x.UpdatedAt}
            )
            ?? throw new NotFoundException($"Category with Id '{context.Message.CategoryId}' was not found.");

        await context.RespondAsync<CategoryRecordResult>(category);
    }
}
