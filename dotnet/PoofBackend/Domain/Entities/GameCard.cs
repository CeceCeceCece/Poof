namespace Domain.Entities
{
    public class GameCard
    {
        public string Id { get; set; }
        public Card Card { get; set; }
        public GameCard()
        {
        }
        public GameCard(string id, Card card)
        {
            Id = id;
            Card = card;
        }
    }
}
