using MediatR;

namespace BuildingBlocks.Commons.CQRS;
public interface IQueryHandler<in TQuery, TReponse> : IRequestHandler<TQuery, TReponse>
    where TQuery : IQuery<TReponse>
    where TReponse : notnull
{
}
