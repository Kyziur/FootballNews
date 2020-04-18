using FootballNews.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballNews.Infrastructure.Data.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date);
            builder.HasOne(x => x.AwayTeam)
                .WithMany(x => x.AwayMatches)
                .HasForeignKey(x => x.AwayTeamId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.HomeTeam).WithMany(x => x.HomeMatches).HasForeignKey(x => x.HomeTeamId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}