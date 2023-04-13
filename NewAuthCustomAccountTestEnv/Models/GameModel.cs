namespace NewAuthCustomAccountTestEnv.Models
{
    public class GameModel
    {
        #region Fields

        private readonly string _default = "/Images/Cursed-Coins-Curse.png";

        #endregion Fields

        #region Public Constructors

        public GameModel(string image1, string image2, string image3, int coinsEarned)
        {
            Image1 = image1 ?? _default;
            Image2 = image2 ?? _default;
            Image3 = image3 ?? _default;
            CoinsEarned = coinsEarned;
        }

        #endregion Public Constructors

        #region Properties

        public int CoinsEarned { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }

        #endregion Properties
    }
}