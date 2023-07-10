using BuildingBlocks.Commons.Models;
using BuildingBlocks.Events.Users;
using Follows.Features.Dtos;
using Follows.Features.Interfaces;
using MassTransit;
using MediatR;

namespace Follows.Features.Queries.GetUsersFollowings.v1;

sealed class GetUsersFollowingsQueryHandler : IRequestHandler<GetUsersFollowingsQuery, PaginatedResults<UserDto>>
{
    private readonly IRequestClient<GetUserByIdRecord> _client;
    private readonly IFollowRepository _followRepository;
    public GetUsersFollowingsQueryHandler(IRequestClient<GetUserByIdRecord> client, IFollowRepository followRepository)
    {
        _client = client;
        _followRepository = followRepository;
    }
    public async Task<PaginatedResults<UserDto>> Handle(GetUsersFollowingsQuery request, CancellationToken cancellationToken)
    {
        var user = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(request.UserId));

        var (results, pageData) = await _followRepository.GetUserFollowersOrFollowings(
            request.UserId,
            request.Page, 
            request.PageSize,
            false 
            );

        return new PaginatedResults<UserDto>(results, pageData);
    }
}