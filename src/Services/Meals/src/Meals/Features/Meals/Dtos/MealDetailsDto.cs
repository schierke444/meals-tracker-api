namespace Meals.Features.Meals.Dtos;

public sealed record MealDetailsDto 
{
    public Guid Id { get; init; }
    public required string MealName {get; init;} 
    public string? MealReview {get; init;}
    public int Rating {get; init;} 
    public CategoryDetailsDto? Category { get; set; } 
    public UserDetailsDto? Owner {get; set;}
    public DateTime CreatedAt {get; init;}
    public  DateTime UpdatedAt {get; init;}
}