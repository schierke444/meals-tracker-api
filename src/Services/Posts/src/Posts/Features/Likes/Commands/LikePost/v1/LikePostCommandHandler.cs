using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using MassTransit;
using MediatR;
using Posts.Commons.Interfaces;
using Posts.Entities;
using Posts.Features.Likes.Interfaces;
using Posts.Features.Posts.Interfaces;

namespace Posts.Features.Likes.Commands.LikePost.v1;

sealed class LikePostCommandHandler : ICommandHandler<LikePostCommand, Unit>
{
    private readonly ILikePostsRepository _likePostsRepository;
    private readonly IRequestClient<GetUserByIdRecord> _userClient;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersPostsService _usersPostsService;
    private readonly IPostRepository _postRepository;
    public LikePostCommandHandler(ILikePostsRepository likePostsRepository, IRequestClient<GetUserByIdRecord> userClient, ICurrentUserService currentUserService, IUsersPostsService usersPostsService, IPostRepository postRepository)
    {
        _likePostsRepository = likePostsRepository;
        _userClient = userClient;
        _currentUserService = currentUserService;
        _usersPostsService = usersPostsService;
        _postRepository = postRepository;
    }
    public async Task<Unit> Handle(LikePostCommand request, CancellationToken cancellationToken)
    {
        // check if the user exists
        var user = await _userClient.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            _currentUserService.UserId ?? throw new UnauthorizedAccessException()
        ));

        // check if the post exists
        var post = await _postRepository.GetPostById(request.PostId)
            ?? throw new NotFoundException($"Post with Id '{request.PostId}' was not found.");

        // Check if User already liked the existing Post
        var existingLikes = await _likePostsRepository.GetValue(
            x => x.PostId.ToString() == request.PostId &&
            x.OwnerId.ToString() == _currentUserService.UserId
        );

        if(existingLikes is not null)
            throw new ConflictException($"User already liked this Post with Id '{request.PostId}'");

        var usersLikes = await _usersPostsService.CreateUsersRecord(user.Message.Id, user.Message.Username);

        LikedPosts likedPosts = new()
        {
            PostId = post.Id, 
            OwnerId = usersLikes.UserId
        };

        await _likePostsRepository.Add(likedPosts);
        await _likePostsRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}