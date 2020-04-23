using System;
using System.ComponentModel.DataAnnotations;

namespace FootballNews.WebApp.Areas.Admin.ViewModels.League
{
    public class LeagueModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Nazwa ligi jest wymagana")]
        public string Name { get; set; }
    }
}