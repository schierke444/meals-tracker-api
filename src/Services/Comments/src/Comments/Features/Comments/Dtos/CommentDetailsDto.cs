namespace Comments.Features.Comments.Dtos;

public sealed class CommentDetailsDto
{
    public Guid Id { get; set; } 
    public required string Content { get; set; }
    public required UserDetailsDto Owner { get; set; }
    public DateTime CreatedAt { get; set; }
}