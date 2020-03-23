using System;
using System.Collections.Generic;

namespace FootballNews.Core.Domain
{
    public class Team
    {
        private readonly IList<Goal> _lostGoals = new List<Goal>();
        private readonly IList<Player> _players = new List<Player>();
        private readonly IList<Goal> _scoredGoals = new List<Goal>();
        private readonly IList<string> _thropies = new List<string>();

        protected Team()
        {
        }

        public Team(Guid id, string name)
        {
            GuardExtensions.ThrowIfNull(id, nameof(id));
            Id = id;
            SetName(name);
        }

        public void SetName(string name)
        {
            GuardExtensions.ThrowIfEmpty(name, nameof(name));
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; private set; }
        public int Points { get; private set; }
        public League League { get; private set; }
        public IEnumerable<Player> Players => _players;
        public IEnumerable<string> Trophies => _thropies;
        public IEnumerable<Goal> ScoredGoals => _scoredGoals;
        public IEnumerable<Goal> LostGoals => _lostGoals;
    }
}