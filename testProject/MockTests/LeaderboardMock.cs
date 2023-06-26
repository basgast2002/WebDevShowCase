using Xunit;

namespace testProject.MockTests
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
                    new LeaderboardModel ( 1, "John Doe", (long)10),
                    new LeaderboardModel(  2, "Jane Doe", (long)2 )
                });

            var userService = new LeaderboardService(mockUserRepository.Object);

            // Act
            var result = userService.Create();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => u.LeaderboardUserName == "John Doe");
            Assert.Contains(result, u => u.LeaderboardUserName == "Jane Doe");
        }

        #endregion Public Methods
    }
}