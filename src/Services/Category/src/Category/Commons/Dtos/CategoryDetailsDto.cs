namespace Category.Commons.Dtos;

public sealed record CategoryDetailsDto(Guid Id, string Name, DateTime CreatedAt, DateTime UpdatedAt): CategoryDto(Id, Name);