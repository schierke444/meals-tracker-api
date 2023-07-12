using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Commons.Models;

namespace Posts.Entities;

[Table("posts")]
public sealed class Post : BaseEntity
{
    [Column("content")]
    public required string Content { get; set; }
    [Column("owner_id")]
    public Guid OwnerId { get; set; }
    public UsersPosts? UsersPosts { get; set; }
    public ICollection<LikedPosts> Likes { get; set; } = new List<LikedPosts>();
}
