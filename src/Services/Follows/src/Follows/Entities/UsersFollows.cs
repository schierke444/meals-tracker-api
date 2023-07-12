using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Follows.Entities;

[Table("users_follows")]
public sealed class UsersFollows
{
    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }
    [Required]
    [Column("username")] 
    public required string Username { get; set; }
    public ICollection<Follows> Followers { get; set; } = new List<Follows>();
    public ICollection<Follows> Followings { get; set; } = new List<Follows>();
}