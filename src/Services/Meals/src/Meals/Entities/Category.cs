using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Commons.Models;

namespace Meals.Entities;

[Table("category")]
public sealed class Category : BaseEntity
{
    [Column("name")]
    public required string Name { get; set; }
    public ICollection<MealCategory> MealCategories {get; set; } = new List<MealCategory>();
}