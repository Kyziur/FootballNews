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

        public Player(Guid id, string firstName, string lastName,string position, DateTime birthdate, Team team)
        {
            GuardExtensions.ThrowIfEmpty(firstName, nameof(firstName));
            GuardExtensions.ThrowIfEmpty(lastName, nameof(lastName));
            GuardExtensions.ThrowIfEmpty(position, nameof(position));
            GuardExtensions.ThrowIfNull(id, nameof(id));
            GuardExtensions.ThrowIfNull(team, nameof(team));
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            Birthdate = birthdate;
            Team = team;
        }

        public void SetName(string firstName, string lastName)
        {
            GuardExtensions.ThrowIfEmpty(firstName, nameof(firstName));
            GuardExtensions.ThrowIfEmpty(lastName, nameof(lastName));
            FirstName = firstName;
            LastName = lastName;
        }

        public void SetBirthdate(DateTime birthdate)
        {
            
            Birthdate = birthdate;
        }

        public void TransferTo(Team team)
        {
            GuardExtensions.ThrowIfNull(team, nameof(team));
            Team = team;
        }
        
        public void SetHeight(int height)
        {
            Height = height;
        }

        public void SetNumberOnTShirt(int number)
        {
            NumberOnTShirt = number;
        }
        public Guid Id { get; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime Birthdate { get; private set; }
        public int Height { get; private set; }
        public string Position { get; private set; }
        public int NumberOnTShirt { get; private set; }
        public byte[] Photo { get; private set; }
        public Team Team { get; private set; }
        public IEnumerable<Goal> Goals { get; set; }
        public IEnumerable<string> LikedBy => _likedBy;
    }
}