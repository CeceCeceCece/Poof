using System.Collections.Generic;

namespace Application.Models.DTOs
{
    public class OptionDto
    {
        public string UserId { get; set; }
        public List<string> CardIds { get; set; }
    }
}
