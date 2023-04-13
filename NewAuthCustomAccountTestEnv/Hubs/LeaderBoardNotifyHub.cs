using Microsoft.AspNetCore.SignalR;

namespace NewAuthCustomAccountTestEnv.Hubs
{
    public class LeaderBoardNotifyHub : Hub
    {
        #region Public Constructors

        public LeaderBoardNotifyHub() : base()
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task UpdateConnectedLeaderboard()
        {
            await Clients.All.SendAsync("UpdateLeaderBoard", CancellationToken.None);
        }

        #endregion Public Methods

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