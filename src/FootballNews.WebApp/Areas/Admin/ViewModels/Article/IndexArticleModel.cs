using System;
using System.Collections.Generic;

namespace FootballNews.WebApp.Areas.Admin.ViewModels.Article
{
    public class IndexArticleModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public byte[] ImageAsBytes { get; set; }
        public IList<string> Tags { get; set; }
        public string League { get; set; }
        public string Author { get; set; }
        public string Created { get; set; }
    }
}