using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Posts.Entities;

[Table("users_posts")]
public class UsersPosts
{
    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }
    [Required]
    [Column("username")] 
    public required string Username { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<LikedPosts> LikedPosts { get; set; } = new List<LikedPosts>();
}