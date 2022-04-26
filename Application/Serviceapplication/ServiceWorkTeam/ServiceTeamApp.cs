using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Route.Domain.Model;

namespace Serviceapplication.ServiceWorkTeam
{
    public class ServiceTeamApp
    {
        static readonly HttpClient clientTeam = new HttpClient();

        public static void PostWorkTeam(WorkTeam team)
        {
            try
            {
                clientTeam.PostAsJsonAsync("https://localhost:44321/api/Team/", team);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static async Task<List<WorkTeam>> GetWorkTeam()
        {
            try
            {
                HttpResponseMessage response = await clientTeam.GetAsync("https://localhost:44321/api/Team");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var teamJson = JsonConvert.DeserializeObject<List<WorkTeam>>(responseBody);
                return teamJson;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public static async Task <List<WorkTeam>> SeachCityTeam(string city)
        {
            try
            {
                HttpResponseMessage response = await clientTeam.GetAsync("https://localhost:44321/api/Team/cidade/team/" + city);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var cityJson = JsonConvert.DeserializeObject<List<WorkTeam>>(responseBody);
                return cityJson;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<WorkTeam> SeachWorkTeam(string id)
        {
            try
            {
                HttpResponseMessage response = await clientTeam.GetAsync("https://localhost:44321/api/Team/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var teamJson = JsonConvert.DeserializeObject<WorkTeam>(responseBody);
                return teamJson;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static void UpdateWorkTeam(string id, WorkTeam team)
        {
            clientTeam.PutAsJsonAsync("https://localhost:44321/api/Team/" + id, team).Wait();
        }


        public static void DeleteWorkTeam(string id)
        {
            clientTeam.DeleteAsync("https://localhost:44321/api/Team/" + id).Wait();
        }
       
    }
}
