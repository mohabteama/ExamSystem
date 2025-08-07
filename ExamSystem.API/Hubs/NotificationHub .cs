using Microsoft.AspNetCore.SignalR;

namespace ExamSystem.API.Hubs
{
    public class NotificationHub : Hub
    {
        public static int TotalViews { get; set; } = 0;
        public async Task NewWondowLoaded() 
        {
            TotalViews++;
            await Clients.All.SendAsync("updatedTotalViews", TotalViews);
        }
    }
}
