namespace testProject1
{
    public class CPYGameController
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

        #endregion Fields

        #region Public Methods

        public int CalculatePayout(string img1, string img2, string img3)
        {
            int payout = 0;

            if (img1 == _images[6] || img1 == _images[5] || img1 == _images[4])
            {
                payout++;
            }
            if (img2 == _images[6] || img2 == _images[5] || img2 == _images[4])
            {
                payout++;
            }
            if (img3 == _images[6] || img3 == _images[5] || img3 == _images[4])
            {
                payout++;
            }

            if (img1 == img2 && img2 == img3 && img3 == img1)
            {
                if (img1 == _images[0])
                {
                    payout += 1000;
                }
                if (img1 == _images[1])
                {
                    payout += -20;
                }
                if (img1 == _images[2])
                {
                    payout = -200;
                }
                if (img1 == _images[3])
                {
                    payout += 50;
                }
                if (img1 == _images[4])
                {
                    payout += 12;
                }
                if (img1 == _images[5])
                {
                    payout += 7;
                }
                if (img1 == _images[6])
                {
                    payout += 2;
                }
            }

            return payout;
        }

        #endregion Public Methods
    }
}