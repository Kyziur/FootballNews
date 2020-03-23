using System;
using System.Collections.Generic;

namespace FootballNews.Core.Domain
{
    public class League
    {
        private IList<Article> _articles = new List<Article>();
        // private IList<Game> _games = new List<Game>();
        private IList<Team> _teams = new List<Team>();

        protected League()
        {
        }

        public League(Guid id, string name)
        {
            GuardExtensions.ThrowIfNull(id, nameof(id));
            Id = id;
            SetName(name);
        }

        public Guid Id { get; }
        public string Name { get; private set; }

        public void SetName(string name)
        {
            GuardExtensions.ThrowIfEmpty(name, nameof(name));
            Name = name;
        }

        public IEnumerable<Article> Articles { get; private set; }
        public IEnumerable<Team> Teams { get; private set; }
    }
}