using FootballNews.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballNews.Infrastructure.Data.Configuration
{
    public class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
    {
        public void Configure(EntityTypeBuilder<ArticleTag> builder)
        {
            builder.HasKey(x => new {x.ArticleId, x.TagId});
            builder.HasOne(x => x.Article)
                .WithMany(x => x.ArticlesTags)
                .HasForeignKey(x => x.ArticleId);
            builder.HasOne(x => x.Tag)
                .WithMany(x => x.TagArticles)
                .HasForeignKey(x => x.TagId);
        }
    }
}