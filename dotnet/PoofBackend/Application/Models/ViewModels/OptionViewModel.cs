using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class OptionViewModel
    {
        public string Description { get; set; }
        public List<string> PossibleTargets{ get; set; }
        public List<CardViewModel> PossibleCards { get; set; }
        public bool? RequireCards { get; set; }
        public int NumberOfCards { get; set; }
        public bool? RequireAnswear { get; set; }
    }
}
