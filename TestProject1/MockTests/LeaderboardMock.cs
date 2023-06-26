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
            // Arrange
            // Er moet getest worden of de methode GetUsers in de klasse UserService de
            // (juiste) gebruikers returned. Deze heeft echter een afhankelijkheid: IUserRepository
            // Mock IUserRepository, zodat de test uitgevoerd kan worden.

            var mockUserRepository = new Mock<ILeaderBoardInterface>();
            mockUserRepository.Setup(repo => repo.Create())
                .Returns(new List<LeaderboardModel>
                {
                    new LeaderboardModel ( 1, "appleman", (long)10),
                    new LeaderboardModel( 2, "ppoo", (long)11)
                });

            var userService = new LeaderboardService(mockUserRepository.Object);

            // Act
            var result = userService.Create();

            // Assert
            Assert.Equal(2, result.Count());
            //Assert.Contains(result, u => u.LeaderboardUserName == "appleman");
            //Assert.Contains(result, u => u.LeaderboardUserName == "ppoo");
            Assert.Collection(result,
                               u => Assert.Equal("appleman", u.LeaderboardUserName),
                               u => Assert.Equal("ppoo", u.LeaderboardUserName));
        }

        #endregion Public Methods
    }
}