using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FootballNews.WebApp.Extensions.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FootballNews.WebApp.Areas.Admin.ViewModels.Article
{
    public class ArticleModel
    {
        public ArticleModel()
        {
            Tags = new List<SelectListItem>();
            Leagues = new List<SelectListItem>();
        }
        
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public byte[] ImageAsBytes { get; set; }
        public IFormFile Image { get; set; }
        public IList<SelectListItem> Tags { get; set; }
        [MinimumElements(1, ErrorMessage = "Wymagany jest przynajmniej 1 tag.")]
        public IEnumerable<string> SelectedTagsIds { get; set; }
        public IList<SelectListItem> Leagues {get; set;}
        [Required(ErrorMessage = "Liga jest wymagana")]
        [DisplayName("Liga")]
        public string SelectedLeagueId { get; set; }


    }
}