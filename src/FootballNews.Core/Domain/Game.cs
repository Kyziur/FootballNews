using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FootballNews.Core.Domain
{
    public class Game
    {
        private const int WinPoints = 3;
        private const int DrawPoints = 1;
        protected Game()
        {
        }

        public Game(Guid id, Team homeTeam, Team awayTeam, DateTime date)
        {
            GuardExtensions.ThrowIfNull(id, nameof(id));
            Id = id;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Date = date;
        }

        public Guid Id { get; private set; }
        public DateTime Date { get; private set; }
        public string Report { get; private set; }

        public void SetReport(string report)
        {
            Report = report;
        }

        public void UpdateTeamsPoints()
        {
            if (HomeTeamScore() == AwayTeamScore())
            {
                HomeTeam.AddPoints(DrawPoints);
                AwayTeam.AddPoints(DrawPoints);
                return;
            }

            if (HomeTeamScore() > AwayTeamScore())
            {
                HomeTeam.AddPoints(WinPoints);
            }
            else
            {
                AwayTeam.AddPoints(WinPoints);
            }
        }
        
        public IEnumerable<Goal> Goals { get; set; }
        public Guid HomeTeamId { get; private set; }
        public Team HomeTeam { get; private set; }
        public Guid AwayTeamId { get; private set; }
        public Team AwayTeam { get; private set; }

        public int HomeTeamScore() => Goals.Count(x => x.Team.Id == HomeTeam.Id);
        public int AwayTeamScore() => Goals.Count(x => x.Team.Id == AwayTeam.Id);


    }
}