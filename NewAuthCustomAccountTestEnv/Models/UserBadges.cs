using System.ComponentModel.DataAnnotations.Schema;

namespace NewAuthCustomAccountTestEnv.Models
{
    public class UserBadges
    {
        #region Fields

        private string _badgeId;
        private string _userId;

        #endregion Fields

        private string _unlockedAt;

        public string UnlockedAt
        {
            get { return _unlockedAt; }
            set { _unlockedAt = value; }
        }

        #region Properties

        [ForeignKey(nameof(BadgeModel.Id))]
        public string BadgeId
        {
            get { return _badgeId; }
            set { _badgeId = value; }
        }

        [ForeignKey(name: nameof(UserModel.Id))]
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        #endregion Properties
    }
}