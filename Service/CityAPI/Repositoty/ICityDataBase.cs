namespace CityAPI.Repositoty
{
    public interface ICityDataBase
    {
        string CityCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
