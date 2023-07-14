using Comments.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comments.Persistence.Configurations;
public sealed class UsersCommentsConfig : IEntityTypeConfiguration<UsersComments>
{
    public void Configure(EntityTypeBuilder<UsersComments> builder)
    {
        builder
            .HasMany(x => x.Comments)
            .WithOne(x => x.UsersComments)
            .HasForeignKey(x => x.OwnerId);
    }
}
