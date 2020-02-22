using System;
using System.Collections.Generic;

namespace FootballNews.Core.Domain
{
    public class League
    {
        private IList<Article> _articles = new List<Article>();
        private IList<Game> _games = new List<Game>();
        private IList<Team> _teams = new List<Team>();

        protected League()
        {
        }

        public League(string name)
        {
            GuardExtensions.ThrowIfEmpty(name, nameof(name));
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }

        public IEnumerable<Article> Articles { get; }
        public IEnumerable<Game> Games { get; }
        public IEnumerable<Team> Teams { get; }
    }
}