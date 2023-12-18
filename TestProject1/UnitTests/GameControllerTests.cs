using testProject1;

namespace TestProject1.UnitTests
{
    public class GameControllerTests
    {
        #region Fields

        private readonly string[] _images = {
            "/Images/Cursed_Coins_Gem.png",
            "/Images/pirate-ship.png",
            "/Images/Cursed_Coins_Curse.png",
            "/Images/storm.png",
            "/Images/fish.png",
            "/Images/chicken-leg.png",
            "/Images/dollar.png"
        };

        private readonly CPYGameController gameController;

        #endregion Fields

        #region Public Constructors

        public GameControllerTests()
        {
            gameController = new CPYGameController();
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public void CalculateMixed()
        {
            // Arrange

            var img1 = _images[2];
            var img2 = _images[1];
            var img3 = _images[0];
            var expectedPayout = 0;

            // Act
            var actualPayout = gameController.CalculatePayout(img1, img2, img3);

            // Assert
            Assert.Equal(expectedPayout, actualPayout);
            // Arrange

            img1 = _images[1];
            img2 = _images[5];
            img3 = _images[6];
            expectedPayout = 2;

            // Act
            actualPayout = gameController.CalculatePayout(img1, img2, img3);

            // Assert
            Assert.Equal(expectedPayout, actualPayout);

            // Arrange

            img1 = _images[6];
            img2 = _images[2];
            img3 = _images[4];
            expectedPayout = 2;

            // Act
            actualPayout = gameController.CalculatePayout(img1, img2, img3);

            // Assert
            Assert.Equal(expectedPayout, actualPayout);
        }

        [Fact]
        public void CalculateTripple()
        {
            // Arrange

            var img1 = _images[0];
            var img2 = _images[0];
            var img3 = _images[0];
            var expectedPayout = 1000;

            // Act
            var actualPayout = gameController.CalculatePayout(img1, img2, img3);

            // Assert
            Assert.Equal(expectedPayout, actualPayout);

            // Arrange

            img1 = _images[1];
            img2 = _images[1];
            img3 = _images[1];
            expectedPayout = -20;

            // Act
            actualPayout = gameController.CalculatePayout(img1, img2, img3);

            // Assert
            Assert.Equal(expectedPayout, actualPayout);

            // Arrange

            img1 = _images[2];
            img2 = _images[2];
            img3 = _images[2];
            expectedPayout = -200;

            // Act
            actualPayout = gameController.CalculatePayout(img1, img2, img3);

            // Assert
            Assert.Equal(expectedPayout, actualPayout);

            // Arrange

            img1 = _images[3];
            img2 = _images[3];
            img3 = _images[3];
            expectedPayout = 50;

            // Act
            actualPayout = gameController.CalculatePayout(img1, img2, img3);

            // Assert
            Assert.Equal(expectedPayout, actualPayout);

            // Arrange

            img1 = _images[4];
            img2 = _images[4];
            img3 = _images[4];
            expectedPayout = 15;

            // Act
            actualPayout = gameController.CalculatePayout(img1, img2, img3);

            // Assert
            Assert.Equal(expectedPayout, actualPayout);
        }

        #endregion Public Methods
    }
}