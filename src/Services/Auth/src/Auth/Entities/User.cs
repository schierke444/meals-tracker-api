using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Commons.Models;

namespace Auth.Entities;

[Table("users")]
public class User : BaseEntity
{
    [Column("username")]
    public required string Username { get; set; }
    [Column("password")]
    public required string Password { get; set; }
    [Column("salt")]
    public required string Salt { get; set; }
}
