using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FootballNews.WebApp.Areas.Admin.ViewModels.Team
{
    public class TeamModel
    {
        public TeamModel()
        {
            Leagues = new List<SelectListItem>();
        }

        public Guid Id { get; set; }
        [Required] public string Name { get; set; }
        [DisplayName("League")]
        [Required] public string SelectedLeagueId { get; set; }
        public IList<SelectListItem> Leagues { get; set; }
    }
}