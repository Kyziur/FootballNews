namespace FootballNews.Core.Domain
{
    public class Card
    {
        public Card(CardColor color, string gaveTo)
        {
            GuardExtensions.ThrowIfNull(color, nameof(color));
            GuardExtensions.ThrowIfEmpty(gaveTo, nameof(gaveTo));
            Color = color;
            For = gaveTo;
        }

        public CardColor Color { get; }
        public string For { get; }
    }
}