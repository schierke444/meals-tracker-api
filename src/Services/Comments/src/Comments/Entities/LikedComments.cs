using BuildingBlocks.Commons.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comments.Entities;

[Table("liked_comments")]
public sealed class LikedComments : BaseEntity
{
    [Column("comment_id")]
    public Guid CommentId { get; set; }
    public Comment? Comment { get; set; }
    [Column("owner_id")] 
    public Guid OwnerId { get; set; }
    public UsersComments? Users { get; set; }
}
