using System.Collections.Generic;
using MongoDB.Driver;
using Route.Domain.Model;
using WorkteamAPI.Repository;

namespace WorkteamAPI.Service
{
    public class ServiceWorkTeam
    {
        private readonly IMongoCollection<WorkTeam> _workTeam;

        public ServiceWorkTeam(IWorkteamDataBase settings)
        {
            var team = new MongoClient(settings.ConnectionString);
            var database = team.GetDatabase(settings.DatabaseName);
            _workTeam = database.GetCollection<WorkTeam>(settings.WorkteamCollectionName);
        }

        public List<WorkTeam> Get() =>
       _workTeam.Find(workTeam => true).ToList();

        public WorkTeam GetId(string id) =>
            _workTeam.Find(workTeam => workTeam.Id == id).FirstOrDefault();

        public WorkTeam GetName(string name) =>
            _workTeam.Find<WorkTeam>(workTeam => workTeam.Name == name).FirstOrDefault();
        public List<WorkTeam> GetCity(string city) =>
         _workTeam.Find(workTeam => workTeam.City == city).ToList();
     
        public  WorkTeam Create(WorkTeam workTeam)
        {
            _workTeam.InsertOne(workTeam);
            return workTeam;
        }

        public void Update(string id, WorkTeam updateWorkTeam) =>
            _workTeam.ReplaceOne(workTeam => workTeam.Id == id, updateWorkTeam);
        public void Remove(string id) =>
            _workTeam.DeleteOne(workTeam => workTeam.Id == id);
    }
}
