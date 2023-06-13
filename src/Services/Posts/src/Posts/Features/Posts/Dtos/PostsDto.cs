namespace Posts.Features.Posts.Dtos;

public record PostsDto(Guid Id, string Content, Guid OwnerId, DateTime CreatedAt);