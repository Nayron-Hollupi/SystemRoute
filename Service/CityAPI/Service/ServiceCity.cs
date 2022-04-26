using System.Collections.Generic;
using CityAPI.Repositoty;
using MongoDB.Driver;
using Route.Domain.Model;

namespace CityAPI.Service
{
    public class ServiceCity
    {
        private readonly IMongoCollection<City> _city;

        public ServiceCity(ICityDataBase settings)
        {
            var city = new MongoClient(settings.ConnectionString);
            var database = city.GetDatabase(settings.DatabaseName);
            _city = database.GetCollection<City>(settings.CityCollectionName);
        }

             public List<City> Get() =>
            _city.Find(city => true).ToList();

        public City GetId(string id) =>
            _city.Find(city => city.Id == id).FirstOrDefault();

        public City GetName(string name) =>
            _city.Find<City>(city => city.Name == name).FirstOrDefault();

        public City Create(City city)
        {
            _city.InsertOne(city);
            return city;
        }

        public void Update(string id, City updateCity) =>
            _city.ReplaceOne(city => city.Id == id, updateCity);
        public void Remove(string id) =>
            _city.DeleteOne(city => city.Id == id);
    }
}

