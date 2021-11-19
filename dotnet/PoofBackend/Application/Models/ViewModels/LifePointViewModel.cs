using Domain.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
