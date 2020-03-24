using System.Collections.Generic;
using FootballNews.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace FootballNews.Infrastructure.Data.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LikedBy).HasConversion(
                v => JsonConvert.SerializeObject(v,
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore}),
                v => JsonConvert.DeserializeObject<IEnumerable<string>>(v,
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore}));
            builder.HasOne(x => x.ParentComment);
            builder.HasOne(x => x.Author).WithMany();
            builder.Property(x => x.CreatedAt);
        }
    }
}