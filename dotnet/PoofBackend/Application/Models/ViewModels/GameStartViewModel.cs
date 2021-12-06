using Domain.Constants.Enums;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class GameStartViewModel
    {
        public string SelfId { get; set; }
        public string Name { get; set; }
        public RoleType Role { get; set; }
        public int LifePoint { get; set; }
        public string SheriffId { get; set; }
        public List<CardViewModel> Cards{ get; set; }
        public List<CharacterViewModel> Characters { get; set; }
    }
}
