using System.ComponentModel;

namespace NewAuthCustomAccountTestEnv.Models
{
    public class LeaderboardModel
    {
        private string _leaderboardUserName;
        private long _coins;
        private int _position;

        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }
        [DisplayName("User")]
        public string LeaderboardUserName
        {
            get { return _leaderboardUserName; }
            set { _leaderboardUserName = value; }
        }

        public long Coins
        {
            get { return _coins; }
            set { _coins = value; }
        }

        public LeaderboardModel(int position, string leaderboardUserName, long coins)
        {
            Position = position;
            LeaderboardUserName = leaderboardUserName;
            Coins = coins;
        }
    }
}