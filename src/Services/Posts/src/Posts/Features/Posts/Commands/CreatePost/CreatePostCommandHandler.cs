
using BuildingBlocks.Commons.CQRS;
using Posts.Entities;
using Posts.Features.Posts.Repositories;

namespace Posts.Features.Posts.Commands.CreatePost;

sealed class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, Guid>
{
    private readonly IPostRepository _postRepository;
    public CreatePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        Post newPost = new()
        {
            Id = Guid.NewGuid(),
            Content = request.Content,
            OwnerId  = request.OwnerId
        };

        await _postRepository.Create(newPost);
        await _postRepository.SaveChangesAsync(cancellationToken);

        return newPost.Id;
    }
}
