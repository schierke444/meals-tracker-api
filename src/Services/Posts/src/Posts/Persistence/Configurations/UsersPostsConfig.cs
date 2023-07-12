using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posts.Entities;

namespace Posts.Persistence.Configurations;

public sealed class UsersPostsConfig : IEntityTypeConfiguration<UsersPosts>
{
    public void Configure(EntityTypeBuilder<UsersPosts> builder)
    {
        builder
            .HasMany(x => x.Posts)
            .WithOne(x => x.UsersPosts)
            .HasForeignKey(x => x.OwnerId);
    }
}