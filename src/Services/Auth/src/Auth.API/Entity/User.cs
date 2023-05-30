using BuildingBlocks.Commons.Models;

namespace Auth.API.Entity;

public class User : BaseEntity
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}
