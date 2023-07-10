using Meals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meals.Persistence.Configurations;

public class MealCategoryConfig : IEntityTypeConfiguration<MealCategory>
{
    public void Configure(EntityTypeBuilder<MealCategory> builder)
    {
        builder
            .HasOne(x => x.Meal)
            .WithMany(x => x.MealCategories)
            .HasForeignKey(x => x.MealId);

        builder
            .HasOne(x => x.Category)
            .WithMany(x => x.MealCategories)
            .HasForeignKey(x => x.CategoryId);
    }
}