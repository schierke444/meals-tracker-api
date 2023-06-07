using MediatR;

namespace BuildingBlocks.Commons.CQRS;
public interface IQuery<out T> : IRequest<T> where T : notnull 
{
}
