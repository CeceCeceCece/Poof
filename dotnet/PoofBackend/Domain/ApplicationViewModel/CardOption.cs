using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ApplicationViewModel
{
    public class CardOption
    {
        public string Description { get; set; }
        public List<string> PossibleTargets { get; set; }
        public bool? RequireCards { get; set; }
        public bool? RequireAnswear { get; set; }
    }
}
