using Meals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meals.Persistence.Configurations;

public sealed class LikedMealsConfig : IEntityTypeConfiguration<LikedMeals>
{
    public void Configure(EntityTypeBuilder<LikedMeals> builder)
    {
        builder
            .HasOne(x => x.Meal)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.MealId);

        
        builder
            .HasOne(x => x.UsersMeals)
            .WithMany(x => x.LikedMeals)
            .HasForeignKey(x => x.OwnerId);
    }
}