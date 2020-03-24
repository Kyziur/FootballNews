using System.Collections.Generic;
using FootballNews.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace FootballNews.Infrastructure.Data.Configuration
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LikedBy).HasConversion(
                v => JsonConvert.SerializeObject(v,
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore}),
                v => JsonConvert.DeserializeObject<IEnumerable<string>>(v,
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore}));
            builder.Ignore(x => x.Tags);
            builder.HasOne(x => x.Author).WithMany(x => x.CreatedArticles);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdatedAt);
        }
    }
}