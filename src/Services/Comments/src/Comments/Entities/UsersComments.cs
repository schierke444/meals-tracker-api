using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comments.Entities;

[Table("users_comments")]
public sealed class UsersComments
{
    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }
    [Required]
    [Column("username")] 
    public required string Username { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();   
    public ICollection<LikedComments> LikedComments { get; set; } = new List<LikedComments>();   
}
