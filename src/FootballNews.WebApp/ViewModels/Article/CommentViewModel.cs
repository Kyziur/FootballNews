using System;

namespace FootballNews.WebApp.ViewModels.Article
{
    public class CommentViewModel
    {
        public string ArticleTitle { get; set; }
        public Guid? Id { get; set; }
        public string Text { get; set; }
        public Guid? ParentId { get; set; }
    }
}