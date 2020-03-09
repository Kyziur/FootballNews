using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FootballNews.WebApp.Areas.Admin.ViewModels.Article
{
    public class ArticleModel
    {
        public ArticleModel()
        {
            Tags = new List<SelectListItem>();
        }
        
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public byte[] ImageAsBytes { get; set; }
        public IFormFile Image { get; set; }
        public IList<SelectListItem> Tags { get; set; }
        public IEnumerable<string> SelectedTagsIds { get; set; }


    }
}