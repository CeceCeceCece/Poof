namespace Application.ViewModels
{
    public class CardIdViewModel
    {
        public CardIdViewModel(string cardId, string characterId)
        {
            CardId = cardId;
            CharacterId = characterId;
        }

        public string CardId { get; set; }
        public string CharacterId { get; set; }
    }
}
