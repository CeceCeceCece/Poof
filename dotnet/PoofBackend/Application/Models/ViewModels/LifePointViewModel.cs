namespace Application.ViewModels
{
    public class LifePointViewModel
    {
        public LifePointViewModel(string characterId, int lifePoint)
        {
            CharacterId = characterId;
            LifePoint = lifePoint;
        }

        public string CharacterId { get; set; }
        public int LifePoint { get; set; }
    }
}
