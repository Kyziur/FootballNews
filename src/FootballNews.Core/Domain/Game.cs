using System;
using System.Collections.Generic;

namespace FootballNews.Core.Domain
{
    public class Game
    {
        private IList<Card> _cards = new List<Card>();

        private IList<Goal> _goals = new List<Goal>();

        protected Game()
        {
        }

        public Game(Team team1, Team team2, DateTime date)
        {
            GuardExtensions.ThrowIfNull(team1, nameof(team1));
            GuardExtensions.ThrowIfNull(team2, nameof(team2));
            SetDate(date);
        }

        public Guid Id { get; }
        public Team Team1 { get; }
        public Team Team2 { get; }
        public string Winner { get; private set; }
        public IEnumerable<Goal> Goals { get; private set; }
        public IEnumerable<Card> Cards { get; private set; }
        public string Report { get; private set; }
        public DateTime Date { get; private set; }

        public void SetDate(DateTime date)
        {
            Date = date;
        }

        public void SetReport(string report)
        {
            GuardExtensions.ThrowIfEmpty(report, nameof(report));
            Report = report;
        }
    }
}