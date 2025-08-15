using HahnSoftware.Domain.Entities;
using HahnSoftware.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HahnSoftware.Infrastructure.Persistence.Configurations;

public class PostTypeConfiguration : EntityTypeConfiguration<Post>
{
    public override void Configure(EntityTypeBuilder<Post> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.Slug)
            .IsUnique(true);

        builder.Property(x => x.Slug)
            .IsRequired(true)
            .HasConversion(x => x.Trim().ToLowerInvariant(), x => x);

        builder.Property(x => x.Title)
            .IsRequired(true)
            .HasMaxLength(556)
            .HasConversion(new StringConverter());
        
        builder.Property(x => x.Body)
            .IsRequired(true);

        // Relations
        builder.HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Bookmarks)
            .WithOne(b => b.Post)
            .HasForeignKey(b => b.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Reactions)
            .WithOne(r => r.Post)
            .HasForeignKey(r => r.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Attachments)
            .WithOne(a => a.Post)
            .HasForeignKey(a => a.PostId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
