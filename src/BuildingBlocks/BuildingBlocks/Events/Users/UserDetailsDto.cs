namespace BuildingBlocks.Events.Users;

public class UserDetailsDto
{
    public Guid Id { get; set; }
    public required string OwnerName{ get; set; }
}
