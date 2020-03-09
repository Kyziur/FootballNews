using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballNews.Core.Domain
{
    public class Article
    {
        protected Article()
        {
        }

        public Article(Guid articleId, string title, string content)
        {
            Id = articleId;
            SetTitle(title);
            SetContent(content);
            CreatedAt = DateTime.UtcNow;
        }

        private IList<ArticleTag> _articleTags = new List<ArticleTag>();
        private IList<Comment> _comments = new List<Comment>();
        private IList<string> _likedBy = new List<string>();

        public Guid Id { get; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public byte[] Image { get; private set; }
        public string ImageName { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; private set; }
        public IEnumerable<ArticleTag> ArticlesTags => _articleTags;
        public IEnumerable<Comment> Comments => _comments;
        public IEnumerable<string> LikedBy => _likedBy;
        public IEnumerable<Tag> Tags => _articleTags.Select(x => x.Tag);


        public void SetTitle(string title)
        {
            GuardExtensions.ThrowIfEmpty(title, nameof(title));
            Title = title;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetContent(string content)
        {
            GuardExtensions.ThrowIfEmpty(content, nameof(content));

            Content = content;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetImage(byte[] image, string imageName)
        {
            GuardExtensions.ThrowIfNull(image, nameof(image));

            if (image.Length <= 0) throw new ArgumentException("Specified image is invalid.", nameof(image));

            Image = image;
            ImageName = imageName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetTags(IEnumerable<Tag> tags)
        {
            var articleTags = tags?.Select(t => new ArticleTag(this, t)).ToList();
            _articleTags = articleTags;
        }
    }
}