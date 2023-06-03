using BuildingBlocks.Commons.Models;

namespace Ingredients.API.Entities;

public class Ingredient : BaseEntity 
{
    public required string Name { get; set; }
}