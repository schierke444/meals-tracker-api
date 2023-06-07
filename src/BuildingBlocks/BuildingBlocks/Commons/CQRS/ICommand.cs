using MediatR;

namespace BuildingBlocks.Commons.CQRS;
public interface ICommand<out T> : IRequest<T>
{
}
