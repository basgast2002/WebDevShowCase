using NewAuthCustomAccountTestEnv.Models;

namespace NewAuthCustomAccountTestEnv.Interfaces
{
    public interface ILeaderBoardInterface
    {
        #region Public Methods

        List<LeaderboardModel> Create();

        #endregion Public Methods
    }
}