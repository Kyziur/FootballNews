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

        public Comment(Guid commentId, User author, string text)
        {
            GuardExtensions.ThrowIfNull(author, nameof(author));
            GuardExtensions.ThrowIfNull(commentId, nameof(commentId));
            Id = commentId;
            Author = author;
            SetText(text);
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; }
        public User Author { get; }
        public string Text { get; private set; }
        public DateTime CreatedAt { get; }
        public IEnumerable<string> LikedBy { get; private set; }
        
        public Comment ParentComment { get; private set; }

        public void SetText(string text)
        {
            GuardExtensions.ThrowIfEmpty(text, nameof(text));
            Text = text;
        }

        public void SetParent(Comment parentComment)
        {
            GuardExtensions.ThrowIfNull(parentComment, nameof(parentComment));
            ParentComment = parentComment;
        }
    }
}