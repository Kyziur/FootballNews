using System;

namespace FootballNews.Core.Domain
{
    public class ArticleTag
    {
        protected ArticleTag()
        {
        }

        public ArticleTag(Article article, Tag tag)
        {
            GuardExtensions.ThrowIfNull(article, nameof(article));
            GuardExtensions.ThrowIfNull(tag, nameof(tag));

            Article = article;
            ArticleId = article.Id;
            Tag = tag;
            TagId = TagId;
        }

        public Guid ArticleId { get; }
        public Article Article { get; }
        public Guid TagId { get; }
        public Tag Tag { get; }
    }
}