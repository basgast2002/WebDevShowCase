namespace NewAuthCustomAccountTestEnv.Models
{
    public class BadgeModel
    {
        #region Fields

        private string _Id;

        private string _imageUrl;
        private string _name;

        private int _unlockedAt;

        #endregion Fields

        #region Properties

        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int UnlockedAt
        {
            get { return _unlockedAt; }
            set { _unlockedAt = value; }
        }

        #endregion Properties
    }
}