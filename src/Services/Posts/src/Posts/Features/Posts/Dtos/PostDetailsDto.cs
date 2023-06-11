namespace Posts.Features.Posts.Dtos;

public record PostDetailsDto (Guid Id, string Content, DateTime Created_At, DateTime Updated_At, Guid Owner_Id)
    : PostsDto(Id, Content, Owner_Id, Created_At);
