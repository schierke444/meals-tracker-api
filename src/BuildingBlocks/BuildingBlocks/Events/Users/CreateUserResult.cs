namespace BuildingBlocks.Events.Users;

public sealed record CreateUserResult(Guid Id, string Username, string Role);