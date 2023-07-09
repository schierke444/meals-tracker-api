using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Commons.Models;

namespace Users.Entities;

[Table("roles")]
public sealed class Roles : BaseEntity
{
    [Column("name")]
    public required string Name { get; set; }
    public ICollection<User> Users {get; set; } = new List<User>();
}