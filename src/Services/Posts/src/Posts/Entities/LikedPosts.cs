using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Commons.Models;

namespace Posts.Entities;

[Table("liked_posts")]
public class LikedPosts : BaseEntity
{
    [Column("post_id")]
    public Guid PostId { get; set; }
    public Post? Post { get; set; }
    [Column("owner_id")] 
    public Guid OwnerId { get; set; }
    public UsersPosts? UsersPosts { get; set; }
}