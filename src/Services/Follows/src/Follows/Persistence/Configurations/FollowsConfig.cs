using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Follows.Persistence.Configurations;

public class FollowsConfig : IEntityTypeConfiguration<Entities.Follows>
{
    public void Configure(EntityTypeBuilder<Entities.Follows> builder)
    {
        builder
            .HasOne(x => x.Follower)
            .WithMany(x => x.Followings)
            .HasForeignKey(x => x.FollowerId);

        builder
            .HasOne(x => x.Followee)
            .WithMany(x => x.Followers)
            .HasForeignKey(x => x.FolloweeId);
    }
}