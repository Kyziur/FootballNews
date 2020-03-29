using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FootballNews.WebApp.Areas.Admin.ViewModels.Player
{
    public class PlayerModel
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        [Required] public int Height { get; set; } = 100;
        [Required]
        public int NumberOnShirt { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string SelectedTeamId { get; set; }
        public IList<SelectListItem> Teams { get; set; }
    }
}