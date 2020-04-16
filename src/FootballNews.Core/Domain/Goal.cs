using System;

namespace FootballNews.Core.Domain
{
    public class Goal
    {
        protected Goal() { }
        public Goal(Guid id, Player shooter, double time, Game game)
        {
            GuardExtensions.ThrowIfNull(id, nameof(id));
            Shooter = shooter;
            Team = shooter.Team;
            Game = game;
            Time = time;
        }

        public Guid Id { get; private set; }
        public Player Shooter { get; private set; }
        public Team Team { get; protected set; }
        public Game Game { get; private set; }
        public double Time { get; private set; }
    }
}