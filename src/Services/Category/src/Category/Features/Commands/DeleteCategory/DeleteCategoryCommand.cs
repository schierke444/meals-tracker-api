using MediatR;

namespace Category.Features.Commands.DeleteCategory;

public sealed record DeleteCategoryCommand(string CategoryId) : IRequest 
{
    
}