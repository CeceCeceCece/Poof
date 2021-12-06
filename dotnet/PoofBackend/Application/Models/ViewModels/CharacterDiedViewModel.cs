using Domain.Constants.Enums;

namespace Application.ViewModels
{
    public class CharacterDiedViewModel
    {
        public CharacterDiedViewModel(string userId, RoleType role)
        { 
            UserId = userId;
            Role = role;
        }        
        public string UserId { get; }
        public RoleType Role { get; }
    }
}
