using HahnSoftware.Domain.Entities;
using HahnSoftware.Infrastructure.Persistence.Converters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HahnSoftware.Infrastructure.Persistence.Configurations;
public sealed class UserTypeConfiguration : EntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.Mail)
            .IsUnique(true);
        
        builder.HasIndex(x => x.Key)
            .IsUnique(true);

        builder.Property(x => x.Mail)
            .IsRequired(true)
            .HasConversion(x => x.Trim().ToLowerInvariant(), x => x);

        builder.Property(x => x.FirstName)
            .IsRequired(true)
            .HasMaxLength(256)
            .HasConversion(new StringConverter());
        
        builder.Property(x => x.LastName)
            .IsRequired(true)
            .HasMaxLength(256)
            .HasConversion(new StringConverter());
        
        builder.Property(x => x.Password)
            .IsRequired(true);

        builder.Property(x => x.Key)
            .IsRequired(true)
            .HasMaxLength(256);

        // Relations
        builder.HasMany(u => u.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Comments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Bookmarks)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
