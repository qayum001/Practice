using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice.Data.Model;
using System.Reflection.Emit;

namespace Practice.Data.Configurations
{
    public class PostCfg : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(e => e.Id);
            builder
                .HasMany(e => e.Comments)
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .IsRequired();
            builder
                .HasMany<Like>()
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .IsRequired();
            builder
                .HasMany(e => e.Tags)
                .WithMany(e => e.Posts);

        }
    }
}
