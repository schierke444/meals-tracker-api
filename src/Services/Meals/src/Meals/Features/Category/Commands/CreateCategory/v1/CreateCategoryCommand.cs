using BuildingBlocks.Commons.CQRS;

namespace Meals.Features.Category.Commands.CreateCategory.v1;

public sealed record  CreateCategoryCommand(string Name) : ICommand<Guid>;