using HahnSoftware.Domain.Entities;
using HahnSoftware.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HahnSoftware.Infrastructure.Persistence;

public class HahnSoftwareDbContext : DbContext
{
    public HahnSoftwareDbContext(DbContextOptions<HahnSoftwareDbContext> options) : base(options)
    {
        
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<PostReaction> PostReactions { get; set; }
    public DbSet<CommentReaction> CommentReactions { get; set; }
    public DbSet<PostBookmark> PostBookmarks { get; set; }
    public DbSet<PostAttachment> PostAttachments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        new CommentTypeConfiguration().Configure(modelBuilder.Entity<Comment>());
        new CommentReactionTypeConfiguration().Configure(modelBuilder.Entity<CommentReaction>());

        new PostTypeConfiguration().Configure(modelBuilder.Entity<Post>());
        new PostBookmarkTypeConfiguration().Configure(modelBuilder.Entity<PostBookmark>());
        new PostAttachmentTypeConfiguration().Configure(modelBuilder.Entity<PostAttachment>());
        new PostReactionTypeConfiguration().Configure(modelBuilder.Entity<PostReaction>());

        new RefreshTokenTypeConfiguration().Configure(modelBuilder.Entity<RefreshToken>());
        new UserTypeConfiguration().Configure(modelBuilder.Entity<User>());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        // optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);

        optionsBuilder.UseSqlServer(sqlOptions =>
        {
            sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            sqlOptions.CommandTimeout(3000);
        });

        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
}