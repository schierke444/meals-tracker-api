using BuildingBlocks.Commons.Models;

namespace Auth.Entities;

public sealed class Roles : BaseEntity
{
    public required string Name { get; set; }
    public ICollection<User> Users {get; set; } = new List<User>();
}