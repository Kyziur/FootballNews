using System;
using System.ComponentModel.DataAnnotations;

namespace FootballNews.WebApp.Areas.Admin.ViewModels.Tag
{
    public class TagModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwę tagu")]
        public string Name { get; set; }
    }
}