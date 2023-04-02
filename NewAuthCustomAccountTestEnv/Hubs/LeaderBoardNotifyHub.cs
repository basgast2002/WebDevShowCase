using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;


namespace NewAuthCustomAccountTestEnv.Hubs
{
    public class LeaderBoardNotifyHub : Hub
    {
     
        public LeaderBoardNotifyHub()  : base()
        {
            
        }

        public async Task UpdateConnectedLeaderboard()
        { 
            
            
                await Clients.All.SendAsync("UpdateLeaderBoard", CancellationToken.None);
            
            
        }



        /*
        public async Task IWantUpDates()
        {

            await Groups.AddToGroupAsync(Context.ConnectionId, "notify-me");
        
        }
        public async Task IDontWantUpdates()
        {

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "notify-me");
        
        }
        */

    }
}