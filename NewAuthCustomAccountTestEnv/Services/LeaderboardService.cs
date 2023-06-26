using NewAuthCustomAccountTestEnv.Interfaces;
using NewAuthCustomAccountTestEnv.Models;

namespace NewAuthCustomAccountTestEnv.Services
{
    public class LeaderboardService
    {
        #region Fields

        private readonly ILeaderBoardInterface _IleaderboardInterface;

        #endregion Fields

        #region Public Constructors

        public LeaderboardService(ILeaderBoardInterface leaderBoardInterface)
        {
            _IleaderboardInterface = leaderBoardInterface;
        }

        #endregion Public Constructors

        #region Public Methods

        public IEnumerable<LeaderboardModel> Create()
        {
            return _IleaderboardInterface.Create();
        }

        #endregion Public Methods
    }
}