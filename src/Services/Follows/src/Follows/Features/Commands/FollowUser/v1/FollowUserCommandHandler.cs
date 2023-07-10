using MediatR;
using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Services;
using BuildingBlocks.Commons.Exceptions;
using MassTransit;
using BuildingBlocks.Events.Users;
using Follows.Features.Interfaces;

namespace Follows.Features.Commands.FollowUser.v1;

sealed class FollowUserCommandHandler : ICommandHandler<FollowUserCommand, Unit> 
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRequestClient<GetUserByIdRecord> _client;
    private readonly IFollowRepository _followRepository;
    public FollowUserCommandHandler(IFollowRepository followRepository, ICurrentUserService currentUserService, IRequestClient<GetUserByIdRecord> client)
    {
        _followRepository = followRepository;
        _currentUserService = currentUserService;
        _client = client;
    }
    public async Task<Unit> Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            _currentUserService.UserId ?? throw new UnauthorizedAccessException()
        ));
        // var user = await _usersRepository.GetValue(x => x.Id.ToString() == _currentUserService.UserId) 
        //     ?? throw new UnauthorizedAccessException();

        if(_currentUserService.UserId == request.UserToFollowId)
            throw new ConflictException("User cannot follow his/her own.");

        // var userToFollow = await _usersRepository.GetValue(x => x.Id.ToString() == request.UserToFollowId, null, false)
        //     ?? throw new NotFoundException("User was not found");
        var userToFollow = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            request.UserToFollowId 
        ));

        var follow = await _followRepository.GetValue(
            x => x.FollowerId == user.Message.Id &&
            x.FolloweeId.ToString() == request.UserToFollowId);

        if (follow != null)
            throw new ConflictException($"User already Followed this User with Id '{request.UserToFollowId}'");

        var newFollow = new Entities.Follows
        {
            FollowerId = user.Message.Id,
            FollowerName = user.Message.Username,
            FolloweeId = userToFollow.Message.Id,
            FolloweeName =  userToFollow.Message.Username 
        };

        await _followRepository.Add(newFollow);
        await _followRepository.SaveChangesAsync();

        return Unit.Value;
    }
}
