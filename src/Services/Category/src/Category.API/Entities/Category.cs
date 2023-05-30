using BuildingBlocks.Commons.Models;

namespace Category.API.Entities;

public class Category : BaseEntity
{
    public required string Name { get; set; }
}
