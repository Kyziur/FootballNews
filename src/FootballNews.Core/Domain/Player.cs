using System;
using System.Collections.Generic;

namespace FootballNews.Core.Domain
{
    public class Player
    {
        private ISet<string> _likedBy = new HashSet<string>();

        protected Player()
        {
        }

        public Player(string firstName, string lastName, DateTime birthdate)
        {
            GuardExtensions.ThrowIfEmpty(firstName, nameof(firstName));
            GuardExtensions.ThrowIfEmpty(lastName, nameof(lastName));
            Birthdate = birthdate;
        }

        public Guid Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; }
        public int Height { get; private set; }
        public string Position { get; private set; }
        public int NumberOnTShirt { get; private set; }
        public byte[] Photo { get; private set; }
        public IEnumerable<string> LikedBy => _likedBy;
    }
}