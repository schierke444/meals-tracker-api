namespace BuildingBlocks.Events.Users;

public sealed record CreateUserRecord(
    string Username,
    string Password,
    string Email,
    string? FirstName,
    string? LastName
);