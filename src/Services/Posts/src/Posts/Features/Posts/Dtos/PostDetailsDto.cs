namespace Posts.Features.Posts.Dtos;

public record PostDetailsDto (Guid Id, string Content, DateTime CreatedAt, DateTime UpdatedAt, Guid OwnerId)
    : PostsDto(Id, Content, OwnerId, CreatedAt);
