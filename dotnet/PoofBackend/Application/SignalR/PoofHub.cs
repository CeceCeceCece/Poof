using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SignalR
{
    public class PoofHub : Hub
    {
        private readonly PoofTracker tracker;

        public PoofHub(PoofTracker tracker)
        {
            this.tracker = tracker;
        }
        public override Task OnConnectedAsync()
        {
            //tracker.UserConnected(Context.User))
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
