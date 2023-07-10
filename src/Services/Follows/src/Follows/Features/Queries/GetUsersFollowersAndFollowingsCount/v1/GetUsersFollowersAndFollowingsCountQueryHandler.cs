using BuildingBlocks.Events.Users;
using Follows.Features.Dtos;
using Follows.Features.Interfaces;
using MassTransit;
using MediatR;

namespace Follows.Features.Queries.GetUsersFollowersAndFollowingsCount.v1;

sealed class GetFollowsCountByUserIdQueryHandler : IRequestHandler<GetUsersFollowersAndFollowingsCountQuery, FollowersAndFollowingsDto>
{
    private readonly IFollowRepository _followRepository;
    private readonly IRequestClient<GetUserByIdRecord> _client;
    public GetFollowsCountByUserIdQueryHandler(IFollowRepository followRepository, IRequestClient<GetUserByIdRecord> client)
    {
        _followRepository = followRepository;
        _client = client;
    }
    public async Task<FollowersAndFollowingsDto> Handle(GetUsersFollowersAndFollowingsCountQuery request, CancellationToken cancellationToken)
    {
        var user = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(request.userId));
        
        var followsCount = await _followRepository.GetUsersFollowsCount(user.Message.Id.ToString());

        return followsCount;
    }
}
