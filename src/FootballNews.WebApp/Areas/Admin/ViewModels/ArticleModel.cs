using System;

namespace FootballNews.WebApp.Areas.Admin.ViewModels
{
    public class ArticleModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public byte[] Image { get; set; }

    }
}