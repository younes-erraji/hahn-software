using HahnSoftware.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace HahnSoftware.Infrastructure.Persistence;

public class HahnSoftwareDbContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<PostReaction> PostReactions => Set<PostReaction>();
    public DbSet<CommentReaction> CommentReactions => Set<CommentReaction>();
    public DbSet<PostCategory> PostCategories => Set<PostCategory>();
    public DbSet<PostBookmark> PostBookmarks => Set<PostBookmark>();
    public DbSet<PostAttachment> PostAttachments => Set<PostAttachment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}