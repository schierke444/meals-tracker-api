using Comments.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comments.Persistence.Configurations;
public class LikedCommentsConfig : IEntityTypeConfiguration<LikedComments>
{
    public void Configure(EntityTypeBuilder<LikedComments> builder)
    {
        builder
            .HasOne(x => x.Comment)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.CommentId);

        builder
            .HasOne(x => x.Users)
            .WithMany(x => x.LikedComments)
            .HasForeignKey(x => x.OwnerId);
    }
}
