namespace Auth.API.Models;

public sealed class RegisterUserDto
{
    public required string Username{ get; set; }
    public required string Password{ get; set; }
    public required string Email { get; set; }
}
