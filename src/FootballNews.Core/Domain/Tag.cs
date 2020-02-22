using System;
using System.Collections.Generic;

namespace FootballNews.Core.Domain
{
    public class Tag
    {
        private readonly IList<Article> _articles = new List<Article>();

        protected Tag() { }

        public Tag(string name)
        {
            GuardExtensions.ThrowIfEmpty(name, nameof(name));
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
        public IEnumerable<Article> Articles => _articles;
    }
}