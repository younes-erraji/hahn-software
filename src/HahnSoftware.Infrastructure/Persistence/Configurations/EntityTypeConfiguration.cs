using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using HahnSoftware.Domain.Entities.Primitives;
using HahnSoftware.Infrastructure.Persistence.Converters;

namespace HahnSoftware.Infrastructure.Persistence.Configurations;

public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CreationDate)
            .IsRequired(true)
            .HasColumnName("creation_date")
            .HasConversion(new DateTimeConverter())
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.DeletionDate)
            .IsRequired(false)
            .HasColumnName("deletion_date")
            .HasConversion(new DateTimeConverter());
    }
}