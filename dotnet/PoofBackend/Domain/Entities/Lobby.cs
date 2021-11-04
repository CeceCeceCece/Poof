using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Lobby
    {
        public Lobby()
        {
        }
        public Lobby(string name, string vezeto)
        {
            Name = name;
            Vezeto = vezeto;
        }

        [Key]
        public string Name { get; set; }
        public string Vezeto { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    }
}
