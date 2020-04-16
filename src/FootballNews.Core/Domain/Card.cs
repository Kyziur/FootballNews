namespace FootballNews.Core.Domain
{
    public class Card
    {
        protected Card(){}
        public Card(CardColor color, string gaveTo)
        {
            GuardExtensions.ThrowIfNull(color, nameof(color));
            GuardExtensions.ThrowIfEmpty(gaveTo, nameof(gaveTo));
            Color = color;
            For = gaveTo;
        }

        public CardColor Color { get; }
        public Game InGame { get; set; }
        public int Minute { get; set; }
        public int Seconds { get; set; }
        public string For { get; }
    }
}