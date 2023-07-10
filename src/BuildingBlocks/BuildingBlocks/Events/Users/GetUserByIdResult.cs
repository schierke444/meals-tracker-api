namespace BuildingBlocks.Events.Users;

public sealed record GetUserByIdResult(Guid Id, string Username);