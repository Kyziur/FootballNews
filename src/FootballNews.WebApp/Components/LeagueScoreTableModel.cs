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
        public TeamScoreModel(string name, int points, int goals)
        {
            Name = name;
            Points = points;
            Goals = goals;
        }
        
        public string Name { get; set; }
        public int Points { get; set; }
        public int Goals { get; }
    }
}