using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meals.Entities;

[Table("users_meals")]
public class UsersMeals
{
    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }
    [Required]
    [Column("username")] 
    public required string Username { get; set; }
    public ICollection<Meal> Meals { get; set; } = new List<Meal>();   
    public ICollection<LikedMeals> LikedMeals { get; set; } = new List<LikedMeals>();   
}