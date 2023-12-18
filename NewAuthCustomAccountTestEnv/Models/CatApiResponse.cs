namespace NewAuthCustomAccountTestEnv.Models
{
    public class CatApiResponse
    {
        #region Fields

        public string[] breeds;
        public string height;
        public string id;
        public string name;
        public string url;
        public string width;

        #endregion Fields

        #region Public Constructors

        public CatApiResponse()
        {
        }

        public CatApiResponse(string[] breeds, string id, string name, string url, string width, string height)
        {
            this.breeds = breeds;
            this.id = id;
            this.name = name;
            this.url = url;
            this.width = width;
            this.height = height;
        }

        #endregion Public Constructors
    }
}