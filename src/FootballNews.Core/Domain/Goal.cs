namespace FootballNews.Core.Domain
{
    public class Goal
    {
        public Goal(string shooterName, int minute, int seconds)
        {
            GuardExtensions.ThrowIfEmpty(shooterName, nameof(shooterName));
            GuardExtensions.ThrowIfBiggerThan(seconds, 60, nameof(seconds));

            Shooter = shooterName;
            Minute = minute;
            Seconds = seconds;
        }

        public string Shooter { get; }
        public int Minute { get; }
        public int Seconds { get; }
    }
}