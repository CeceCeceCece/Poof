using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string Name{ get; set; }
        public string Owner{ get; set; }
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}
