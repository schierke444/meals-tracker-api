using BuildingBlocks.Commons.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comments.Entities;

[Table("comments")]
public sealed class Comment : BaseEntity
{
    [Column("content")]
    public required string Content { get; set; }
    [Column("post_id")]
    public Guid PostId { get; set; }
    [Column("owner_id")]
    public Guid OwnerId { get; set; }
    public UsersComments? UsersComments { get; set; }
    public ICollection<LikedComments> Likes { get; set; } = new List<LikedComments>();
}
