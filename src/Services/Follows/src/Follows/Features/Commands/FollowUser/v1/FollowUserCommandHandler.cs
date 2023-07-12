using MediatR;
using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Services;
using BuildingBlocks.Commons.Exceptions;
using MassTransit;
using BuildingBlocks.Events.Users;
using Follows.Features.Interfaces;
using Follows.Entities;

namespace Follows.Features.Commands.FollowUser.v1;

sealed class FollowUserCommandHandler : ICommandHandler<FollowUserCommand, Unit>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRequestClient<GetUserByIdRecord> _client;
    private readonly IFollowRepository _followRepository;
    private readonly IUsersFollowsRepository _usersFollowsRepository;
    public FollowUserCommandHandler(IFollowRepository followRepository, ICurrentUserService currentUserService, IRequestClient<GetUserByIdRecord> client, IUsersFollowsRepository usersFollowsRepository)
    {
        _followRepository = followRepository;
        _currentUserService = currentUserService;
        _client = client;
        _usersFollowsRepository = usersFollowsRepository;
    }
    public async Task<Unit> Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            _currentUserService.UserId ?? throw new UnauthorizedAccessException()
        ));

        if (_currentUserService.UserId == request.UserToFollowId)
            throw new ConflictException("User cannot follow his/her own.");

        var userToFollow = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            request.UserToFollowId
        ));

        var userFollower = await CreateUsersRecord(user.Message.Id, user.Message.Username);
        var userFollowee = await CreateUsersRecord(userToFollow.Message.Id, userToFollow.Message.Username);

        var follow = await _followRepository.GetValue(
            x => x.FollowerId == user.Message.Id &&
            x.FolloweeId.ToString() == request.UserToFollowId);

        if (follow is not null)
            throw new ConflictException($"User already Followed this User with Id '{request.UserToFollowId}'");

        var newFollow = new Entities.Follows
        {
            FollowerId = userFollower.UserId,
            FolloweeId = userFollowee.UserId 
        };

        await _followRepository.Add(newFollow);
        await _followRepository.SaveChangesAsync();

        return Unit.Value;
    }

    private async Task<UsersFollows> CreateUsersRecord(Guid Id, string Username)
    {
        var existingUser = await _usersFollowsRepository.GetUserById(Id);

        // if no users_posts record, make a new one, then assigned to usersPosts;
        // else assign to usersPosts
        UsersFollows usersFollows;
        if (existingUser is null)
        {
            var result = await CreateUsersFollowsRecord(Id, Username);
            usersFollows = result;
        }
        else
        {
            usersFollows = existingUser;
        }

        return usersFollows;
    }

    private async Task<UsersFollows> CreateUsersFollowsRecord(Guid Id, string Username, CancellationToken cancellationToken = default)
    {
        UsersFollows newUsersFollows = new()
        {
            UserId = Id,
            Username = Username
        };

        await _usersFollowsRepository.Add(newUsersFollows);
        await _usersFollowsRepository.SaveChangesAsync(cancellationToken);

        return newUsersFollows;
    }
}
