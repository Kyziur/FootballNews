using System.Collections.Generic;

namespace FootballNews.WebApp.Components
{
    public class LeagueScoreTableModel
    {
        public string League { get; set; }
        public IList<TeamScoreModel> TeamScores { get; set; }
    }

    public class TeamScoreModel
    {
        public TeamScoreModel(string name, int points)
        {
            Name = name;
            Points = points;
        }
        
        public string Name { get; set; }
        public int Points { get; set; }
    }
}