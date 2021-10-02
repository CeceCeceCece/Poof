using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SignalR
{
    public class PoofTracker
    {
        private static readonly Dictionary<string, List<string>> ConnectedPlayers = new Dictionary<string, List<string>>();

        public Task UserConnected(string userName, string connectionId) 
        {
            lock (ConnectedPlayers) 
            {
                if (ConnectedPlayers.ContainsKey(userName))
                {
                    ConnectedPlayers[userName].Add(connectionId);
                }
                else
                {
                    ConnectedPlayers.Add(userName, new List<string> { connectionId });
                }
            }
            return Task.CompletedTask;
        }

        public Task UserDisconnected(string userName, string connectionId)
        {
            lock (ConnectedPlayers)
            {
                if (!ConnectedPlayers.ContainsKey(userName))
                    return Task.CompletedTask;

                ConnectedPlayers[userName].Remove(connectionId);
                if (ConnectedPlayers[userName].Count == 0) 
                {
                    ConnectedPlayers.Remove(userName);
                }
 
            }
            return Task.CompletedTask;
        }

        public Task<string[]> GetOnlineUsers() 
        {
            string[] connectedPlayers;
            lock (ConnectedPlayers) 
            {
                connectedPlayers = ConnectedPlayers.OrderBy(k => k.Key).Select(k => k.Key).ToArray();
            }
            return Task.FromResult(connectedPlayers);
        }

        public Task<List<string>> GetConnections(string userName) 
        {
            List<string> connectionIds;
            lock (ConnectedPlayers) 
            {
                connectionIds = ConnectedPlayers.GetValueOrDefault(userName);
            }

            return Task.FromResult(connectionIds);
        }
    }
}
