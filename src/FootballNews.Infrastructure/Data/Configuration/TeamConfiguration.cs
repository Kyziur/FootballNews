using FootballNews.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballNews.Infrastructure.Data.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.Points);
            builder.HasMany(x => x.Players).WithOne(x => x.Team);
            builder.HasMany(x => x.AwayMatches).WithOne(x => x.AwayTeam);
            builder.HasMany(x => x.HomeMatches).WithOne(x => x.HomeTeam);
        }
    }
}