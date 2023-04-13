using System.ComponentModel;

namespace NewAuthCustomAccountTestEnv.Models
{
    public class LeaderboardModel
    {
        #region Fields

        private long _coins;
        private string _leaderboardUserName;
        private int _position;

        #endregion Fields

        #region Public Constructors

        public LeaderboardModel(int position, string leaderboardUserName, long coins)
        {
            Position = position;
            LeaderboardUserName = leaderboardUserName;
            Coins = coins;
        }

        #endregion Public Constructors

        #region Properties

        public long Coins
        {
            get { return _coins; }
            set { _coins = value; }
        }

        [DisplayName("User")]
        public string LeaderboardUserName
        {
            get { return _leaderboardUserName; }
            set { _leaderboardUserName = value; }
        }

        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        #endregion Properties
    }
}