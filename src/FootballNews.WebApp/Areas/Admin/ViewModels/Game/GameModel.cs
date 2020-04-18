using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FootballNews.WebApp.Areas.Admin.ViewModels.Game
{
    public class GameModel
    {
        public Guid Id { get; set; }
        public string Report { get; set; }
        public IList<SelectListItem> Teams { get; set; }
        [DisplayName("Gospodarze")]
        public string SelectedHomeTeam { get; set; }
        [DisplayName("Goście")]
        public string SelectedAwayTeam { get; set; }
        public IList<GoalModel> HomeTeamGoals { get; set; }
        public IList<GoalModel> AwayTeamGoals { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }

    public class GoalModel
    {
        public string ShooterName { get; set; }
        public string Shooter { get; set; }
        public double Time { get; set; }
    }
}