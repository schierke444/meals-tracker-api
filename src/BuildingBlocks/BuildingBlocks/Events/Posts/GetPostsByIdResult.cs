using BuildingBlocks.Events.Users;

namespace BuildingBlocks.Events.Posts;

public sealed class GetPostsByIdResult
{
    public Guid Id { get; set; }
    public required string Content {get; set;}
    public required UserDetailsDto Owner {get; set;}
    public DateTime CreatedAt {get; set;}
}