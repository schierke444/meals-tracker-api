using Meals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meals.Persistence.Configurations;

public class MealIngredientConfig : IEntityTypeConfiguration<MealIngredients>
{
    public void Configure(EntityTypeBuilder<MealIngredients> builder)
    {
        builder
            .HasOne(x => x.Ingredients)
            .WithMany(x => x.MealIngredient)
            .HasForeignKey(x => x.IngredientId);

        builder
            .HasOne(x => x.Meals)
            .WithMany(x => x.MealIngredient)
            .HasForeignKey(x => x.MealId);
    }
}