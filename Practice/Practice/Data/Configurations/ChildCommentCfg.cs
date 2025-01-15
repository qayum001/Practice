using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice.Data.Model;

namespace Practice.Data.Configurations
{
    public class ChildCommentCfg : IEntityTypeConfiguration<ChildCommentId>
    {
        public void Configure(EntityTypeBuilder<ChildCommentId> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
