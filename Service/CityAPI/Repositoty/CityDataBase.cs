namespace CityAPI.Repositoty
{
    public class CityDataBase : ICityDataBase
    {
        public string CityCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
