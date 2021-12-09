using System.Collections.Generic;

namespace Application.ViewModels
{
    public class OptionViewModel
    {
        public string Description { get; set; }
        public List<string> PossibleTargets { get; set; }
        public bool RequireCards { get; set; }
    }
}
