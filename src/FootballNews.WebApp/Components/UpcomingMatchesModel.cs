using System;
using System.Collections.Generic;

namespace FootballNews.WebApp.Components
{
    public class UpcomingMatchesModel
    {
        public IEnumerable<MatchModel> Matches { get; set; }
    }

    public class MatchModel
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateTime Date { get; set; }
        
    }
}