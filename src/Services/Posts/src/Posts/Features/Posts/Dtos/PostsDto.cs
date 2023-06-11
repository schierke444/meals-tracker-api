namespace Posts.Features.Posts.Dtos;

public record PostsDto(Guid Id, string Content, Guid Owner_Id, DateTime Created_At);