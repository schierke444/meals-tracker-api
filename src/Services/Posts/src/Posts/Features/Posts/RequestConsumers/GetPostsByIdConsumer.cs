using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events.Posts;
using BuildingBlocks.Events.Users;
using MassTransit;
using Posts.Features.Posts.Interfaces;

namespace Posts.Features.Posts.RequestConsumers;

public sealed class GetPostsByIdConsumer : IConsumer<GetPostsByIdRecord>
{
    private readonly IPostRepository _postRepository;
    public GetPostsByIdConsumer(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task Consume(ConsumeContext<GetPostsByIdRecord> context)
    {
        var post = await _postRepository.GetPostById(context.Message.Id.ToString())
            ?? throw new NotFoundException($"Post with Id '{context.Message.Id}' was not found.");

        await context.RespondAsync<GetPostsByIdResult>(post);
    }
}