using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace testProject
{
    [TestClass]
    public class UnitTest1
    {
        #region Fields

        private readonly CPYGameController _controller;

        private readonly string[] _images = {
            "/Images/Cursed_Coins_Gem.png",
            "/Images/pirate-ship.png",
            "/Images/Cursed_Coins_Curse.png",
            "/Images/storm.png",
            "/Images/fish.png",
            "/Images/chicken-leg.png",
            "/Images/dollar.png"
        };

        #endregion Fields

        #region Public Constructors

        public UnitTest1()
        {
            _controller = new CPYGameController();
        }

        #endregion Public Constructors

        #region Public Methods

        [TestMethod]
        public void TestPayoutCalculatorNoMatches()
        {
            int expected = 0;
            int result = _controller.CalculatePayout(_images[1], _images[0], _images[1]);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestPayoutCalculatorSmallEarnings()
        {
            int expected = 1;
            int result = _controller.CalculatePayout(_images[6], _images[0], _images[0]);
            Assert.AreEqual(expected, result);

            result = _controller.CalculatePayout(_images[0], _images[6], _images[0]);
            Assert.AreEqual(expected, result);

            result = _controller.CalculatePayout(_images[0], _images[0], _images[6]);
            Assert.AreEqual(expected, result);

            result = _controller.CalculatePayout(_images[5], _images[0], _images[0]);
            Assert.AreEqual(expected, result);

            result = _controller.CalculatePayout(_images[0], _images[5], _images[0]);
            Assert.AreEqual(expected, result);

            result = _controller.CalculatePayout(_images[0], _images[0], _images[5]);
            Assert.AreEqual(expected, result);

            result = _controller.CalculatePayout(_images[4], _images[0], _images[0]);
            Assert.AreEqual(expected, result);

            result = _controller.CalculatePayout(_images[0], _images[4], _images[0]);
            Assert.AreEqual(expected, result);

            result = _controller.CalculatePayout(_images[0], _images[0], _images[4]);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestPayoutCalculatorTripples()
        {
            int expected0 = 1000;
            int result = _controller.CalculatePayout(_images[0], _images[0], _images[0]);
            Assert.AreEqual(expected0, result);

            int expected1 = -20;
            result = _controller.CalculatePayout(_images[1], _images[1], _images[1]);
            Assert.AreEqual(expected1, result);

            int expected2 = -200;
            result = _controller.CalculatePayout(_images[2], _images[2], _images[2]);
            Assert.AreEqual(expected2, result);
            int expected3 = 50;
            result = _controller.CalculatePayout(_images[3], _images[3], _images[3]);
            Assert.AreEqual(expected3, result);

            int expected4 = 15;
            result = _controller.CalculatePayout(_images[4], _images[4], _images[4]);
            Assert.AreEqual(expected4, result);

            int expected5 = 10;
            result = _controller.CalculatePayout(_images[5], _images[5], _images[5]);
            Assert.AreEqual(expected5, result);

            int expected6 = 5;
            result = _controller.CalculatePayout(_images[6], _images[6], _images[6]);
            Assert.AreEqual(expected6, result);
        }

        #endregion Public Methods
    }
}