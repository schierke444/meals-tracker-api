using BuildingBlocks.Commons.Models;

namespace Category.Entities; 

public sealed class Category : BaseEntity
{
    public required string Name { get; set; }
}
