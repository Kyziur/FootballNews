using System;
using System.Collections.Generic;
using System.Linq;

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

        public void AddPoints(int points)
        {
            Points += points;
        }

        public Guid Id { get; }
        public string Name { get; private set; }
        public int Points { get; private set; }
        public League League { get; private set; }
        public IEnumerable<Game> HomeMatches { get; set; }

        public int GetGoals()
        {
            var goalsOnHome = HomeMatches.Count(x => x.Goals.Any(y => y.Team.Id == Id));
            var goalsOnAway = AwayMatches.Count(x => x.Goals.Any(y => y.Team.Id == Id));
            return goalsOnAway + goalsOnHome;
        }
        public IEnumerable<Game> AwayMatches { get; set; }
        public IEnumerable<Player> Players => _players;
        public IEnumerable<string> Trophies => _thropies;
    }
}