using Domain.Constants.Enums;

namespace Application.ViewModels
{
    public class WinnerIsViewModel
    {
        public WinnerIsViewModel(RoleType winner)
        {
            Winner = winner;
        }

        public RoleType Winner{ get; set; }
    }
}
