using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice.Data.Model;

namespace Practice.Data.Configurations
{
    public class LikeCfg : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
