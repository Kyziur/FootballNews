using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FootballNews.WebApp.Areas.Admin.ViewModels.Player
{
    public class PlayerModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Imię jest wymagane")]
        
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Wzrost jest wymagany")]
        public int Height { get; set; } = 100;
        [Required(ErrorMessage = "Numer na koszulce jest wymagany")]
        public int NumberOnShirt { get; set; }
        [Required(ErrorMessage = "Pozycja jest wymagana")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Wybierz drużynę")]
        public string SelectedTeamId { get; set; }
        public IList<SelectListItem> Teams { get; set; }
    }
}