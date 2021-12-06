using Domain.Constants.Enums;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class CharacterViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public int LifePoint { get; set; }
        public List<string> CardIds { get; set; }
    }
}
