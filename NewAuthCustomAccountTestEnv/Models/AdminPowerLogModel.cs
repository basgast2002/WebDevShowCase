namespace NewAuthCustomAccountTestEnv.Models
{
    public class AdminPowerLogModel
    {
        #region Public Constructors

        public AdminPowerLogModel(string id, string actorId, string operation, string target, string dateTimeString)
        {
            Id = id;
            ActorId = actorId;
            Operation = operation;
            Target = target;
            Date = dateTimeString;
        }

        #endregion Public Constructors

        #region Properties

        public string ActorId { get; set; } = "UNKNOWN";
        public string Date { get; set; } = "UNKNOWN";
        public string Id { get; set; } = "UNKNOWN";
        public string Operation { get; set; } = "UNKNOWN";
        public string Target { get; set; } = "UNKNOWN";

        #endregion Properties
    }
}