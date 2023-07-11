namespace Posts.Features.Posts.Dtos;

public sealed class PostDetailsDto
{
    public Guid Id { get; set; }
    public required string Content {get; set;}
    public required UserDetailsDto Owner {get; set;}
    public DateTime CreatedAt {get; set;}
}