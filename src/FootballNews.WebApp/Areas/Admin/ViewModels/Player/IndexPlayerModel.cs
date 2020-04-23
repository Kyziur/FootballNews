using System;
using System.ComponentModel.DataAnnotations;

namespace FootballNews.WebApp.Areas.Admin.ViewModels.Player
{
    public class IndexPlayerModel
    {
        public Guid Id { get; set; }
  
        public string FullName { get; set; }
        public string Team { get; set; }
        public string Position { get; set; }
        public DateTime BirthDate { get; set; }
        public int Height { get; set; }
       

    }
}