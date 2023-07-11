namespace Posts.Features.Posts.Dtos;

public class UserDetailsDto
{
    public Guid Id { get; set; }
    public required string OwnerName{ get; set; }
}