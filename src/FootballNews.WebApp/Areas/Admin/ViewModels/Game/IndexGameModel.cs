using System;

namespace FootballNews.WebApp.Areas.Admin.ViewModels.Game
{
    public class IndexGameModel
    {
        public Guid Id { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public string Date { get; set; }
      
    }
}