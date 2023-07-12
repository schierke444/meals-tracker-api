using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using MassTransit;
using Posts.Commons.Interfaces;
using Posts.Entities;
using Posts.Features.Posts.Interfaces;

namespace Posts.Features.Posts.Commands.CreatePost.v1;

sealed class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, Guid>
{
    private readonly IPostRepository _postRepository;
    private readonly IUsersPostsRepository _usersPostsRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IRequestClient<GetUserByIdRecord> _client;
    private readonly IUsersPostsService _usersPostsService;
    public CreatePostCommandHandler(IPostRepository postRepository, IRequestClient<GetUserByIdRecord> client, ICurrentUserService currentUserService, IUsersPostsRepository usersPostsRepository, IUsersPostsService usersPostsService)
    {
        _postRepository = postRepository;
        _client = client;
        _currentUserService = currentUserService;
        _usersPostsRepository = usersPostsRepository;
        _usersPostsService = usersPostsService;
    }
    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var user = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            _currentUserService.UserId ?? throw new UnauthorizedAccessException()
        ));

        var existingUsersPosts = await _usersPostsRepository.GetUserById(user.Message.Id);

        var usersPosts = await _usersPostsService.CreateUsersRecord(user.Message.Id, user.Message.Username);

        Post newPost = new()
        {
            Id = Guid.NewGuid(),
            Content = request.Content,
            OwnerId = usersPosts.UserId 
        };

        await _postRepository.Add(newPost);
        await _postRepository.SaveChangesAsync(cancellationToken);

        return newPost.Id;
    }
}
