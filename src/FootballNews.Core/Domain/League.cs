using System;
using System.Collections.Generic;

namespace FootballNews.Core.Domain
{
    public class League
    {
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

        public IList<Article> Articles { get; private set; }
        public IList<Team> Teams { get; private set; }
    }
}