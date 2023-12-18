using Moq;
using NewAuthCustomAccountTestEnv.Interfaces;
using NewAuthCustomAccountTestEnv.Models;
using NewAuthCustomAccountTestEnv.Services;

namespace testProject1.MockTests
{
    public class LeaderboardMock
    {
        #region Public Methods

        [Fact]
        public void Create_ShouldReturnAllUsers()
        {
            var mockUserRepository = new Mock<ILeaderBoardInterface>();
            mockUserRepository.Setup(repo => repo.Create())
                .Returns(new List<LeaderboardModel>
                {
                    new LeaderboardModel ( 1, "appleman", (long)10),
                    new LeaderboardModel( 2, "ppoo", (long)9),
                    new LeaderboardModel ( 3, "aco", (long)8),
                    new LeaderboardModel( 4, "ato", (long)7),
                    new LeaderboardModel ( 5, "busman", (long)6),
                    new LeaderboardModel( 6, "Jayden", (long)6)
                });

            var userService = new LeaderboardService(mockUserRepository.Object);

            // Act
            var result = userService.Create();

            // Assert
            Assert.Equal(6, result.Count());
            Assert.Collection(result,
                               u => Assert.Equal("appleman", u.LeaderboardUserName),
                               u => Assert.Equal("ppoo", u.LeaderboardUserName),
                               u => Assert.Equal("aco", u.LeaderboardUserName),
                               u => Assert.Equal("ato", u.LeaderboardUserName),
                               u => Assert.Equal("busman", u.LeaderboardUserName),
                               u => Assert.Equal("Jayden", u.LeaderboardUserName)
                               );

            Assert.Collection(result.OrderByDescending(c => c.Coins),

                               u => Assert.Equal("appleman", u.LeaderboardUserName),
                               u => Assert.Equal("ppoo", u.LeaderboardUserName),
                               u => Assert.Equal("aco", u.LeaderboardUserName),
                               u => Assert.Equal("ato", u.LeaderboardUserName),
                               u => Assert.Equal("busman", u.LeaderboardUserName),
                               u => Assert.Equal("Jayden", u.LeaderboardUserName)
                               );
            Assert.Collection(result.OrderBy(p => p.Position),
                               u => Assert.Equal("appleman", u.LeaderboardUserName),
                               u => Assert.Equal("ppoo", u.LeaderboardUserName),
                               u => Assert.Equal("aco", u.LeaderboardUserName),
                               u => Assert.Equal("ato", u.LeaderboardUserName),
                               u => Assert.Equal("busman", u.LeaderboardUserName),
                               u => Assert.Equal("Jayden", u.LeaderboardUserName)
                               );
        }

        #endregion Public Methods
    }
}