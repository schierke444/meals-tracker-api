
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Entities;

namespace Users.Persistence.Configurations;
internal class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasOne(x => x.UserInfo)
            .WithOne(x => x.User)
            .HasForeignKey<UserInfo>(x => x.UserId);
    }
}
