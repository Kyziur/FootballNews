using System;
using System.Collections.Generic;

namespace FootballNews.Core.Domain
{
    public class Tag
    {
        private IList<ArticleTag> _tagArticles = new List<ArticleTag>();

        protected Tag() { }

        public Tag(Guid tagId, string name)
        {
            Id = tagId;
            SetName(name);
        }

        public Guid Id { get; }
        public string Name { get; private set; }
        public IEnumerable<ArticleTag> TagArticles => _tagArticles;

        public void SetName(string name)
        {
            GuardExtensions.ThrowIfEmpty(name, nameof(name));
            Name = name;
        }
    }
}