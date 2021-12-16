using Domain.Constants.Enums;

namespace Domain.Entities
{
    public class Card
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CardValues Value { get; set; }
        public CardSuits Suite { get; set; }
        public CardType Type { get; set; }
    }
}
