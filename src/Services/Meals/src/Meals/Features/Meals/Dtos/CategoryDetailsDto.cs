namespace Meals.Features.Meals.Dtos;

public record CategoryDetailsDto 
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}