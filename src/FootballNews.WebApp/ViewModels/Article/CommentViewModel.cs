using System;
using System.ComponentModel.DataAnnotations;

namespace FootballNews.WebApp.ViewModels.Article
{
    public class CommentViewModel
    {
        [Required]
        public string ArticleTitle { get; set; }
        public Guid? Id { get; set; }
        [Required]
        public string Text { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FullName { get; set; }
        public bool CreatedByCurrentUser { get; set; }
        public bool CreatedByAdmin { get; set; }
        public bool CurrentUserIsAdmin { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}