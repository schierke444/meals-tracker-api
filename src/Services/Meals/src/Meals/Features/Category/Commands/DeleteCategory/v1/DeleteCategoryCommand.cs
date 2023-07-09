using BuildingBlocks.Commons.CQRS;
using MediatR;

namespace Meals.Features.Category.Commands.DeleteCategory.v1;

public sealed record DeleteCategoryCommand(string CategoryId) : ICommand<Unit> 
{
    
}