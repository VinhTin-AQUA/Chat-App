namespace ChatAppServer.Data
{
    public class MongoSetting
    {
        public string ConnectionUri { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string UserCollectionName { set; get; } = string.Empty;
        public string GroupCollectionName { get; set; } = string.Empty;
        public string MessageCollectionName { get; set; } = string.Empty;
    }
}
