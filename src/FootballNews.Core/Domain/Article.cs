using System;
using System.Collections.Generic;

namespace FootballNews.Core.Domain
{
    public class Article
    {
        protected Article()
        {
        }

        public Article(string title, string content)
        {
            SetTitle(title);
            SetContent(content);
            CreatedAt = DateTime.UtcNow;
        }

        private IList<Tag> _tags => new List<Tag>();
        private IList<Comment> _comments => new List<Comment>();
        private ISet<string> _likedBy => new HashSet<string>();

        public Guid Id { get; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public byte[] Image { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; private set; }
        public IEnumerable<Tag> Tags => _tags;
        public IEnumerable<Comment> Comments => _comments;
        public IEnumerable<string> LikedBy => _likedBy;


        public void SetTitle(string title)
        {
            GuardExtensions.ThrowIfEmpty(title, nameof(title));
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetContent(string content)
        {
            GuardExtensions.ThrowIfEmpty(content, nameof(content));

            Content = content;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetImage(byte[] image)
        {
            GuardExtensions.ThrowIfNull(image, nameof(image));

            if (image.Length <= 0) throw new ArgumentException("Specified image is invalid.", nameof(image));

            Image = image;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}