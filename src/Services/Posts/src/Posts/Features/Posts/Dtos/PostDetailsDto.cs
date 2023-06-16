namespace Posts.Features.Posts.Dtos;

public class PostDetailsDto 
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public UserDetailsDto? Owner{ get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
