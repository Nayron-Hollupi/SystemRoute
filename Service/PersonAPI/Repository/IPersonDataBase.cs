namespace PersonAPI.Repository
{
    public interface IPersonDataBase
    {
        string PersonCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
