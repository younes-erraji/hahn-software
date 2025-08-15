using HahnSoftware.Domain.Entities;
using HahnSoftware.Infrastructure.Persistence.Converters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HahnSoftware.Infrastructure.Persistence.Configurations;

public class CommentTypeConfiguration : EntityTypeConfiguration<Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Content)
            .IsRequired(true)
            .HasConversion(new StringConverter());

        // Relations
        builder.HasMany(c => c.Replies)
            .WithOne(r => r.ThreadComment)
            .HasForeignKey(r => r.ThreadCommentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Reactions)
            .WithOne(cr => cr.Comment)
            .HasForeignKey(cr => cr.CommentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
