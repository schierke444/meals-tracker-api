using BuildingBlocks.Commons.Models;

namespace Posts.Entities;

public sealed class Post : BaseEntity
{
    public required string Content { get; set; }
    public Guid OwnerId { get; set; }
}
