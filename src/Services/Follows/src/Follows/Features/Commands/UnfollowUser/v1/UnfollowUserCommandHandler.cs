using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using Follows.Features.Interfaces;
using MassTransit;
using MediatR;

namespace Follows.Features.Commands.UnfollowUser.v1;

sealed class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRequestClient<GetUserByIdRecord> _client;
    private readonly IFollowRepository _followRepository;
    public UnfollowUserCommandHandler(IFollowRepository followRepository, ICurrentUserService currentUserService, IRequestClient<GetUserByIdRecord> client)
    {
        _followRepository = followRepository;
        _currentUserService = currentUserService;
        _client = client;
    }

    public async Task Handle(UnfollowUserCommand  request, CancellationToken cancellationToken)
    {
        var user = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            _currentUserService.UserId ?? throw new UnauthorizedAccessException()
        ));

        if(_currentUserService.UserId == request.UserToFollowId)
            throw new ConflictException("User cannot unfollow his/her own.");

        var userToFollow = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            request.UserToFollowId 
        ));

        var follow = await _followRepository.GetValue(
            x => x.FollowerId == user.Message.Id &&
            x.FolloweeId.ToString() == request.UserToFollowId, false) 
            ?? throw new NotFoundException("Follow record was not found");

        _followRepository.Delete(follow);
        await _followRepository.SaveChangesAsync();
    }
}