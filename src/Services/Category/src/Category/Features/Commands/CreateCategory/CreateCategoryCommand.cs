using BuildingBlocks.Commons.CQRS;

namespace Category.Features.Commands.CreateCategory;

public sealed record  CreateCategoryCommand(string Name) : ICommand<Guid>;