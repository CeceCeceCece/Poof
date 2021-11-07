using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class OptionDto
    {
        public string UserId { get; set; }
        public List<string> CardIds { get; set; }
    }
}
