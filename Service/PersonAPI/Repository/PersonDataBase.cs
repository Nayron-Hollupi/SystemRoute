namespace PersonAPI.Repository
{
    public class PersonDataBase : IPersonDataBase
    {
       public string PersonCollectionName { get; set; }
       public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
