using BuildingBlocks.Commons.CQRS;

namespace Posts.Features.Posts.Commands.CreatePost.v1;

public record CreatePostCommand(string Content) : ICommand<Guid>;