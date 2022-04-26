namespace WorkteamAPI.Repository
{
    public class WorkTeamDataBase : IWorkteamDataBase
    {
        public string WorkteamCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
