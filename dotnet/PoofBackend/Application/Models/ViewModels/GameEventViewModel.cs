using Domain.Constants.Enums;

namespace Application.ViewModels
{
    public class GameEventViewModel
    {
        public GameEventViewModel(string characterId, CardViewModel card)
        {
            CharacterId = characterId;
            Card = card;
        }
        public string CharacterId { get; set; }
        public CardViewModel Card { get; set; }

    }
}
