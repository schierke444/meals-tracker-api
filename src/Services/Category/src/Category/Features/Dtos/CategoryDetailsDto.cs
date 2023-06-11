namespace Category.Features.Dtos;

public sealed record CategoryDetailsDto(Guid Id, string Name, DateTime Created_At, DateTime Updated_At): CategoryDto(Id, Name);