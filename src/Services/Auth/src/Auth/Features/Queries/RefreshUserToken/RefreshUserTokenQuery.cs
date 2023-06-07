using Auth.Commons.Dtos;
using BuildingBlocks.Commons.CQRS;

namespace Auth.Features.Queries.RefreshUserToken;
public sealed record RefreshUserTokenQuery(string RefreshToken) : IQuery<(AuthDetailsDto, string)>;
