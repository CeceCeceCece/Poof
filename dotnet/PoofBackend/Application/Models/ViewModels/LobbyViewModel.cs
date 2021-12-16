using System.Collections.Generic;

namespace Application.Models.ViewModels
{
    public class LobbyViewModel
    {
        public LobbyViewModel()
        {
        }
        public LobbyViewModel(string name, string owner)
        {
            Name = name;
            Owner = owner;
        }

        public string Name { get; set; }
        public string Owner { get; set; }
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}
