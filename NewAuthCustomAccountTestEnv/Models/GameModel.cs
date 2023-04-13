namespace NewAuthCustomAccountTestEnv.Models
{
    public class GameModel
    {
        private string _default = "/Images/storm.png";
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public int CoinsEarned { get; set; }

        public GameModel(string image1, string image2, string image3, int coinsEarned)
        {
            Image1 = image1 ?? _default;
            Image2 = image2 ?? _default;
            Image3 = image3 ?? _default;
            CoinsEarned = coinsEarned;
        }
    }
}