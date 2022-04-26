using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Route.Domain.Model;

namespace Serviceapplication.ServicePerson
{
    public class ServicePersonApp
    {
        static readonly HttpClient clientPerson = new HttpClient();

    
        public static async Task<Person> GetPersonName(string name)
        {
            try
            {
                HttpResponseMessage response = await clientPerson.GetAsync("https://localhost:44311/api/Person/person/" + name);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var peopleJson = JsonConvert.DeserializeObject<Person>(responseBody);
                return peopleJson;


            }
            catch (Exception)
            {
                throw;
            }
        }

        

        public static void PostPerson(Person person)
        {
            try
            {
                clientPerson.PostAsJsonAsync("https://localhost:44311/api/Person/", person).Wait();

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static async Task<List<Person>> GetPerson()
        {
            try
            {
                HttpResponseMessage response = await clientPerson.GetAsync("https://localhost:44311/api/Person");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var personJson = JsonConvert.DeserializeObject<List<Person>>(responseBody);
                return personJson;


            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Person>> GetPersonStatus()
        {
            try
            {
                HttpResponseMessage response = await clientPerson.GetAsync("https://localhost:44311/status");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var personJson = JsonConvert.DeserializeObject<List<Person>>(responseBody);
                return personJson;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static async Task<Person> SeachPerson(string id)
        {
            try
            {
                HttpResponseMessage response = await clientPerson.GetAsync("https://localhost:44311/api/Person/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var personJson = JsonConvert.DeserializeObject<Person>(responseBody);
                return personJson;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static void UpdatePerson(string id, Person person)
        {
            clientPerson.PutAsJsonAsync("https://localhost:44311/api/Person/" + id, person).Wait();
        }


        public static void DeletePerson(string id)
        {
            clientPerson.DeleteAsync("https://localhost:44311/api/Person/" + id).Wait();
        }



    }
}
