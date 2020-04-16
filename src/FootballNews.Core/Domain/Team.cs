using System;
using System.Collections.Generic;

namespace FootballNews.Core.Domain
{
    public class Team
    {
        private readonly IList<Player> _players = new List<Player>();
        private readonly IList<string> _thropies = new List<string>();

        protected Team()
        {
        }

        public Team(Guid id, string name, League league)
        {
            GuardExtensions.ThrowIfNull(id, nameof(id));
            Id = id;
            SetName(name);
            SetLeague(league);
        }

        public void SetName(string name)
        {
            GuardExtensions.ThrowIfEmpty(name, nameof(name));
            Name = name;
        }

        public void SetLeague(League league)
        {
            GuardExtensions.ThrowIfNull(league, nameof(league));
            League = league;
        }

        private void AddPoints(int points)
        {
            Points += points;
        }

        public Guid Id { get; }
        public string Name { get; private set; }
        public int Points { get; private set; }
        public League League { get; private set; }
        public IEnumerable<Game> HomeMatches { get; set; }
        public IEnumerable<Game> AwayMatches { get; set; }
        public IEnumerable<Player> Players => _players;
        public IEnumerable<string> Trophies => _thropies;
    }
}