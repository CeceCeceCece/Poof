using Domain.Constants.Enums;

namespace Application.ViewModels
{
    public class GameEventViewModel
    {
        public GameEventViewModel(GameEvent gameEvent, string characterId, CardViewModel card)
        {
            Event = gameEvent;
            CharacterId = characterId;
            Card = card;
        }

        public GameEvent Event { get; set; }
        public string CharacterId { get; set; }
        public CardViewModel Card { get; set; }

    }
}
