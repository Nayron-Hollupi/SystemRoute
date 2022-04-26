using System.Collections.Generic;
using MongoDB.Driver;
using PersonAPI.Repository;
using Route.Domain.Model;

namespace PersonAPI.Service
{
    public class ServicePerson
    {
        private readonly IMongoCollection<Person> _person;

        public ServicePerson(IPersonDataBase settings)
        {
            var person = new MongoClient(settings.ConnectionString);
            var database = person.GetDatabase(settings.DatabaseName);
            _person = database.GetCollection<Person>(settings.PersonCollectionName);
        }

        public List<Person> Get() =>
       _person.Find(person => true).ToList();

        public Person GetId(string id) =>
            _person.Find(person => person.Id == id).FirstOrDefault();

        public Person GetName(string name) =>
            _person.Find<Person>(city => city.Name == name).FirstOrDefault();

        public List<Person> GetStatus() =>
        _person.Find(person => person.Status == false).ToList();
        public Person Create(Person person)
        {
            _person.InsertOne(person);
            return person;
        }

        public void Update(string id, Person updatePerson) =>
            _person.ReplaceOne(person => person.Id == id, updatePerson);
        public void Remove(string id) =>
            _person.DeleteOne(person => person.Id == id);
    }
}
