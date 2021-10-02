using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Player : IdentityUser
    {
        public int? GameId { get; set; }
        public Game Game { get; set; }
    }
}
