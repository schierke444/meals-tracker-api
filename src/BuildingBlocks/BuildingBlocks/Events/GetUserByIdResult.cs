namespace BuildingBlocks.Events;

public class GetUserByIdResult
{
    public Guid Id { get; set; } 
    public required string Username { get; set; }
    public required string Email{ get; set; }
}