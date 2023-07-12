using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posts.Entities;

namespace Posts.Persistence.Configurations;

public sealed class LikedPostsConfig : IEntityTypeConfiguration<LikedPosts>
{
    public void Configure(EntityTypeBuilder<LikedPosts> builder)
    {
        builder
            .HasOne(x => x.Post)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.PostId);

        builder
            .HasOne(x => x.UsersPosts)
            .WithMany(x => x.LikedPosts)
            .HasForeignKey(x => x.OwnerId);
    }
}