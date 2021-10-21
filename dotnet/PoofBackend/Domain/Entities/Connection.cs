using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Connection
    {
        public Connection()
        {
        }

        public Connection(string connectionId, string username, string userId)
        {
            ConnectionId = connectionId;
            Username = username;
            UserId = userId;
        }

        public string ConnectionId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
    }
}
