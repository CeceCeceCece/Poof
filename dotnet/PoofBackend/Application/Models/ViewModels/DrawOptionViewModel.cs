using System.Collections.Generic;

namespace Application.ViewModels
{
    public class DrawOptionViewModel
    {
        public DrawOptionViewModel(bool userIdRequired, List<CardViewModel> cards)
        {
            UserIdRequired = userIdRequired;
            Cards = cards;
        }

        public bool UserIdRequired { get; set; }
        public List<CardViewModel> Cards{ get; set; }
    }
}
