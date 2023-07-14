namespace Comments.Features.Comments.Dtos;

public class UserDetailsDto
{
    public Guid Id { get; set; }
    public required string OwnerName{ get; set; }
}