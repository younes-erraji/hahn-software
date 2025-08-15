using HahnSoftware.Domain.Entities;
using HahnSoftware.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HahnSoftware.Infrastructure.Persistence.Configurations;

public class PostAttachmentTypeConfiguration : EntityTypeConfiguration<PostAttachment>
{
    public override void Configure(EntityTypeBuilder<PostAttachment> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.PostId)
            .IsRequired(true);

        builder.Property(x => x.Name)
            .IsRequired(true)
            .HasMaxLength(256)
            .HasConversion(new StringConverter());

        builder.Property(x => x.Extension)
            .IsRequired(true)
            .HasMaxLength(156)
            .HasConversion(new StringConverter());

        builder.Property(x => x.ContentType)
            .IsRequired(true)
            .HasMaxLength(256)
            .HasConversion(new StringConverter());
        
        builder.Property(x => x.Key)
            .IsRequired(true)
            .HasMaxLength(256)
            .HasConversion(new StringConverter());

        builder.HasIndex(x => x.Key)
            .IsUnique(true);
    }
}
