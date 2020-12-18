using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Proba1.SignalRCommunication
{
    public class AvioHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {   
            await Clients.All.SendAsync("ReceiveReservation", user, message);
        }
    }
}