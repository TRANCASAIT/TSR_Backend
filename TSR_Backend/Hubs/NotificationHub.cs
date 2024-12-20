using Microsoft.AspNetCore.SignalR;
namespace TSR_Backend.Hubs
{
    public class NotificationHub: Hub
    {
        public async Task SendNewRecordNotification()
        {
            await Clients.All.SendAsync("NewRecordAdded");
        }

        public async Task NotifyRecordUpdated()
        {
            await Clients.All.SendAsync("RecordUpdated");
        }

        public async Task NotifyDocumentUpdated()
        {
            await Clients.All.SendAsync("RecordDocumentUpdated");
        }

        public async Task NotifyRecordRemoved()
        {
            await Clients.All.SendAsync("RecordRemoved");
        }

        public async Task NotifyMessage()
        {
            await Clients.All.SendAsync("MessageAdded");

        }
    }
}
