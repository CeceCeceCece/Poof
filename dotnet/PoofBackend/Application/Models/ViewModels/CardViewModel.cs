using Domain.Constants.Enums;

namespace Application.ViewModels
{
    public class CardViewModel
    {
        public CardViewModel(string id, string name, CardType type, CardSuits suite, CardValues value)
        {
            Id = id;
            Name = name;
            Type = type;
            Suite = suite;
            Value = value;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public CardType Type { get; set; }
        public CardSuits Suite { get; set; }
        public CardValues Value { get; set; }
    }
}
