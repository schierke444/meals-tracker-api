using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Commons.Models;

namespace Follows.Entities;

[Table("follows")]
public class Follows : BaseEntity
{
    [Column("follower_id")]
    public Guid FollowerId { get; set; }
    [Column("follower_name")]
    public UsersFollows? Follower { get; set; }
    [Column("followee_id")]
    public Guid FolloweeId { get; set; }
    [Column("followee_name")]
    public UsersFollows? Followee { get; set; }
}