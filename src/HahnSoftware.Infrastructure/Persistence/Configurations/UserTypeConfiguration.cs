using HahnSoftware.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HahnSoftware.Infrastructure.Persistence.Configurations;
public sealed class UserTypeConfiguration : EntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.Mail)
            .IsUnique(true);

        builder.Property(x => x.Mail)
            .IsRequired(true)
            .HasConversion(x => x.Trim().ToLowerInvariant(), x => x);
    }
}
