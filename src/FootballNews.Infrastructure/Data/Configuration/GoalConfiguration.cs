using FootballNews.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballNews.Infrastructure.Data.Configuration
{
    public class GoalConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Time);
            builder.HasOne(x => x.Shooter).WithMany(x => x.Goals);
            builder.HasOne(x => x.Game).WithMany(x => x.Goals);

        }
    }
}