using System;
using System.Collections.Generic;

namespace FootballNews.Core.Domain
{
    public class Comment
    {
        private IList<string> _likedBy = new List<string>();

        protected Comment()
        {
        }

        public Comment(string author, string text)
        {
            GuardExtensions.ThrowIfEmpty(author, nameof(author));
            GuardExtensions.ThrowIfEmpty(text, nameof(text));

            Author = author;
            Text = text;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; }
        public string Author { get; }
        public string Text { get; }
        public DateTime CreatedAt { get; }
        public IEnumerable<string> LikedBy { get; private set; }
        
        public Comment ParentComment { get; private set; }
    }
}