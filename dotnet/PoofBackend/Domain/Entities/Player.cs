using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Player : IdentityUser
    {
        public int? GameId { get; set; }
        public Game Game { get; set; }
    }
}
