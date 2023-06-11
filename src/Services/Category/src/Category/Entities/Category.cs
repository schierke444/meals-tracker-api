using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Commons.Models;

namespace Category.Entities; 

[Table("category")]
public sealed class Category : BaseEntity
{
    [Column("name")]
    public required string Name { get; set; }
}
