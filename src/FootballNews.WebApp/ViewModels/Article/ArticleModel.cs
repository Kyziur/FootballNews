using System;
using System.Collections.Generic;

namespace FootballNews.WebApp.ViewModels.Article
{
    public class ArticleModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string League { get; set; }
        public string Content { get; set; }
        public byte[] ImageAsBytes { get; set; }
        public string ImageName { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<Tuple<Guid, string>> TagsIdsWithNames { get; set; }
    }
}