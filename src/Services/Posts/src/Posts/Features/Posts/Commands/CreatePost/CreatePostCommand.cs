using BuildingBlocks.Commons.CQRS;

namespace Posts.Features.Posts.Commands.CreatePost;

public record CreatePostCommand(string Content, Guid OwnerId) : ICommand<Guid>;