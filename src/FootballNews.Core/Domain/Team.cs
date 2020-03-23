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

        public Team(string name)
        {
            GuardExtensions.ThrowIfEmpty(name, nameof(name));
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
        public int Points { get; private set; }
        public League League { get; private set; }
        public IEnumerable<Player> Players => _players;
        public IEnumerable<string> Trophies => _thropies;
        public IEnumerable<Goal> ScoredGoals => _scoredGoals;
        public IEnumerable<Goal> LostGoals => _lostGoals;
    }
}