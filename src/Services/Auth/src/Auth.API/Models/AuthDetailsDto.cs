namespace Auth.API.Models;

public sealed class AuthDetailsDto
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public string AccessToken { get; set; } = string.Empty;
}
