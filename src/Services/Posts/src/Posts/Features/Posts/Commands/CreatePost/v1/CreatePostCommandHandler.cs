using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using MassTransit;
using Posts.Entities;
using Posts.Features.Posts.Interfaces;
using Posts.Features.Posts.Repositories;

namespace Posts.Features.Posts.Commands.CreatePost.v1;

sealed class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, Guid>
{
    private readonly IPostRepository _postRepository;
    private readonly IUsersPostsRepository _usersPostsRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IRequestClient<GetUserByIdRecord> _client;
    public CreatePostCommandHandler(IPostRepository postRepository, IRequestClient<GetUserByIdRecord> client, ICurrentUserService currentUserService, IUsersPostsRepository usersPostsRepository)
    {
        _postRepository = postRepository;
        _client = client;
        _currentUserService = currentUserService;
        _usersPostsRepository = usersPostsRepository;
    }
    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var user = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            _currentUserService.UserId ?? throw new UnauthorizedAccessException()
        ));

        var existingUsersPosts = await _usersPostsRepository.GetUserById(user.Message.Id);

        // if no users_posts record, make a new one, then assigned to usersPosts;
        // else assign to usersPosts
        UsersPosts usersPosts;
        if(existingUsersPosts is null)
        {
            UsersPosts newUsersPosts = new()
            {
                UserId = user.Message.Id,
                Username = user.Message.Username
            };

            await _usersPostsRepository.Add(newUsersPosts);
            await _usersPostsRepository.SaveChangesAsync();

            usersPosts = newUsersPosts;
        }
        else 
        {
            usersPosts = existingUsersPosts;
        } 

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
