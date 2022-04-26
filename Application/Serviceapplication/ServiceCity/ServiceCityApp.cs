using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Route.Domain.Model;

namespace Serviceapplication.ServiceCity
{
    public class ServiceCityApp
    {
        static readonly HttpClient clientCity = new HttpClient();

        public static void PostCity(City city)
        {
            try
            {
                clientCity.PostAsJsonAsync("https://localhost:44332/api/City", city).Wait();

            }
            catch (Exception)
            {
                throw;
            }

        }

            public static async Task<List<City>> GetCity()
            {
                try
                {
                    HttpResponseMessage response = await clientCity.GetAsync("https://localhost:44332/api/City");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var cityJson = JsonConvert.DeserializeObject<List<City>>(responseBody);
                    return cityJson;


                }
                catch (Exception)
                {
                    throw;
                }
            }

        public static async Task<City> SeachCityName(string city)
        {
            try
            {
                HttpResponseMessage response = await clientCity.GetAsync(" https://localhost:44332/api/City/city/" + city);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var cityJson = JsonConvert.DeserializeObject<City>(responseBody);
                return cityJson;


            }
            catch (Exception)
            {
                throw;
            }
        }

       
        public static async Task<City> SeachCity(string id)
        {
            try
            {
                HttpResponseMessage response = await clientCity.GetAsync("https://localhost:44332/api/City/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var cityJson = JsonConvert.DeserializeObject<City>(responseBody);
                return cityJson;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static void UpdateCity(string id, City city)
        {
            clientCity.PutAsJsonAsync("https://localhost:44332/api/City/" + id, city).Wait();
        }


        public static void DeleteCity(string id)
        {
            clientCity.DeleteAsync("https://localhost:44332/api/City/" + id).Wait();
        }




    }
    }
