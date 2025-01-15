using Microsoft.EntityFrameworkCore;
using Practice.Data.Model;
using System.Reflection;

namespace Practice.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<User> User { get; set; }
    public DbSet<Post> Post { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<ChildCommentId> ChildComment { get; set; }
    public DbSet<Like> Like { get; set; }
    public DbSet<Tag> Tag { get; set; }
    public DbSet<UsedToken> UsedToken { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}