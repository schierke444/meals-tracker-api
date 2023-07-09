using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Commons.Models;

namespace Users.Entities;

[Table("users_info")]
public sealed class UserInfo : BaseEntity
{
    [Column("first_name")]
    public string? FirstName { get; set; }
    [Column("last_name")]
    public string? LastName { get; set; }
    [Column("bio")]
    public string? Bio { get; set; }
    [Column("email")]
    public required string Email { get; set; }
    [Column("user_id")]
    public Guid UserId { get; set; }
    public User? User { get; set; }
}