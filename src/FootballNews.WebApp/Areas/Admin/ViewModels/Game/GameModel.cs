using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FootballNews.WebApp.Areas.Admin.ViewModels.Game
{
    public class GameModel
    {
        public Guid Id { get; set; }
        public string Report { get; set; }
      
        public IList<SelectListItem> Teams { get; set; }
        [Required(ErrorMessage = "Drużyna gospodarzy jest wymagana")]
        [DisplayName("Gospodarze")]
        public string SelectedHomeTeam { get; set; }
        [Required(ErrorMessage = "Drużyna gości jest wymagana")]
        [DisplayName("Goście")]
        public string SelectedAwayTeam { get; set; }
        public IList<GoalModel> HomeTeamGoals { get; set; }
        public IList<SelectListItem> HomeTeamPlayers { get; set; }
        public IList<GoalModel> AwayTeamGoals { get; set; }
        public IList<SelectListItem> AwayTeamPlayers { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool HasBeenPlayed { get; set; }
    }

    public class GoalModel
    {
        public string ShooterName { get; set; }
        public string Shooter { get; set; }
        public int Time { get; set; }
    }
}