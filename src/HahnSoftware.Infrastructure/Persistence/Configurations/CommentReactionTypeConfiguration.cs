using HahnSoftware.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HahnSoftware.Infrastructure.Persistence.Configurations;

public class CommentReactionTypeConfiguration : EntityTypeConfiguration<CommentReaction>
{
    public override void Configure(EntityTypeBuilder<CommentReaction> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => new
        {
            x.UserId,
            x.CommentId
        }).IsUnique(true);

        builder.Property(x => x.UserId)
            .IsRequired(true);
        
        builder.Property(x => x.CommentId)
            .IsRequired(true);

        builder.Property(x => x.Type)
            .IsRequired(true);

        // Relations
        builder.HasOne(cr => cr.User)
            .WithMany()
            .HasForeignKey(cr => cr.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
