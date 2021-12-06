using System.Collections.Generic;

namespace Application.ViewModels
{
    public class DrawOptionViewModel
    {
        public DrawOptionViewModel(bool userId, List<CardViewModel> cards)
        {
            UserId = userId;
            Cards = cards;
        }

        public bool UserId { get; set; }
        public List<CardViewModel> Cards{ get; set; }
    }
}
