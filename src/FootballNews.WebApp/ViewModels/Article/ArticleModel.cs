using System;

namespace FootballNews.WebApp.ViewModels.Article
{
    public class ArticleModel
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public byte[] Image { get; set; }
        public Guid TagId { get; set; }
    }
}