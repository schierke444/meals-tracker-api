using Meals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meals.Persistence.Configurations;

public sealed class UsersMealsConfig : IEntityTypeConfiguration<UsersMeals>
{
    public void Configure(EntityTypeBuilder<UsersMeals> builder)
    {
        builder
            .HasMany(x => x.Meals)
            .WithOne(x => x.UsersMeals)
            .HasForeignKey(x => x.OwnerId);
    }
}