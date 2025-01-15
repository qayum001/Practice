using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice.Data.Model;

namespace Practice.Data.Configurations
{
    public class CommentCfg : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasMany(e => e.ChildComments)
                .WithOne()
                .IsRequired()
                .HasForeignKey(e => e.ParentId);
        }
    }
}
