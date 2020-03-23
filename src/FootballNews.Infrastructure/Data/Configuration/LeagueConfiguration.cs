using FootballNews.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballNews.Infrastructure.Data.Configuration
{
    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.HasMany(x => x.Articles).WithOne(x => x.League);
            builder.HasMany(x => x.Teams).WithOne(x => x.League);
        }
    }
}