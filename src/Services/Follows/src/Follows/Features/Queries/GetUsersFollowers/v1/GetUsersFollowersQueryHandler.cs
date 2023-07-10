using BuildingBlocks.Commons.Models;
using BuildingBlocks.Events.Users;
using Follows.Features.Dtos;
using Follows.Features.Interfaces;
using MassTransit;
using MediatR;

namespace Follows.Features.Queries.GetUsersFollowers.v1;

sealed class GetUsersFollowersQueryHandler : IRequestHandler<GetUsersFollowersQuery, PaginatedResults<UserDto>>
{
    private readonly IFollowRepository _followRepository;
    private readonly IRequestClient<GetUserByIdRecord> _client;
    public GetUsersFollowersQueryHandler(IRequestClient<GetUserByIdRecord> client, IFollowRepository followRepository)
    {
        _client = client;
        _followRepository = followRepository;
    }
    public async Task<PaginatedResults<UserDto>> Handle(GetUsersFollowersQuery request, CancellationToken cancellationToken)
    {
        var user = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(request.UserId));

        var (results, pageData) = await _followRepository.GetUserFollowersOrFollowings(
            request.UserId,
            request.Page, 
            request.PageSize,
            true
            );


        return new PaginatedResults<UserDto>(results, pageData);
    }
}