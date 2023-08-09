using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice.Data.Model;
using System.Reflection.Emit;

namespace Practice.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> User { get; set; }
    public DbSet<Post> Post { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<ChildCommentId> ChildComment { get; set; }
    public DbSet<Like> Like { get; set; }
    public DbSet<Tag> Tag { get; set; }
    public DbSet<UsedToken> UsedToken { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Used Token

        modelBuilder.Entity<UsedToken>().HasKey(e => e.Id);

        #endregion

        #region User

        modelBuilder.Entity<User>().HasKey(e => e.Id);
        modelBuilder.Entity<User>()
            .HasMany(e => e.Posts)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired(false);
        modelBuilder.Entity<User>()
            .HasMany(e => e.Comments)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired(false);
        modelBuilder.Entity<User>()
            .HasMany(e => e.Likes)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired(false);

        #endregion

        #region Post

        modelBuilder.Entity<Post>().HasKey(e => e.Id);
        modelBuilder.Entity<Post>()
            .HasMany(e => e.Comments)
            .WithOne(e => e.Post)
            .HasForeignKey(e => e.PostId)
            .IsRequired();
        modelBuilder.Entity<Post>()
            .HasMany<Like>()
            .WithOne(e => e.Post)
            .HasForeignKey(e => e.PostId)
            .IsRequired();
        modelBuilder.Entity<Post>()
            .HasMany(e => e.Tags)
            .WithMany(e => e.Posts);

        #endregion

        #region Tag

        modelBuilder.Entity<Tag>().HasKey(e => e.Id);

        #endregion

        #region Comment
        modelBuilder.Entity<ChildCommentId>().HasKey(e => e.Id);

        // забыл связь для родительского комментария 
        modelBuilder.Entity<ChildCommentId>()
            .HasOne<Comment>()
            .WithMany()
            .HasForeignKey(child => child.ParentId);

        modelBuilder.Entity<Comment>().HasKey(e => e.Id);
        modelBuilder.Entity<Comment>()
            .HasMany(e => e.ChildComments)
            .WithOne()
            .IsRequired()
            .HasForeignKey(e => e.ParentId);

        #endregion

        #region Like

        modelBuilder.Entity<Like>().HasKey(e => e.Id);

        #endregion

        /*
         * писать все ef конфиги в одном методе громоздко, неудобно, 
         * особенно когда ты захочешь все прописывать явно вручную.
         * Так что используется "IEntityTypeConfiguration<>" для +- каждой сущности.
         */

        // такой конфиг можно применить вручную 
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        // или использовать все "IEntityTypeConfiguration<>" что быдут найдемы в переданной сборке (проекте) (Assembly) 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // builder.HasKey(e => e.Id);
        // builder
        //     .HasMany(e => e.Posts)
        //     .WithOne(e => e.User)
        //     .HasForeignKey(e => e.UserId)
        //     .IsRequired(false);
        // builder
        //     .HasMany(e => e.Comments)
        //     .WithOne(e => e.User)
        //     .HasForeignKey(e => e.UserId)
        //     .IsRequired(false);
        // builder
        //     .HasMany(e => e.Likes)
        //     .WithOne(e => e.User)
        //     .HasForeignKey(e => e.UserId)
        //     .IsRequired(false);
    }
}