using System.Collections.Generic;
using CityAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Domain.Model;

namespace CityAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : Controller
    {
        private readonly ServiceCity _serviceCity;

        public CityController(ServiceCity serviceCity)
        {
            _serviceCity = serviceCity;
        }
         [HttpGet]
         public ActionResult<List<City>> GetAll() =>
            _serviceCity.Get();

         [HttpGet("{id}")]
         public ActionResult<City> Getid(string id)
         {
             var seachCity = _serviceCity.GetId(id);

             if (seachCity == null)
                 return BadRequest("Cidade não encontrada, verifique as informações e tente novamente!");

             return seachCity;
         }

         [HttpGet("city/{city}")]
         public ActionResult<City> GetNameCity(string city)
         {
             var seachCity = _serviceCity.GetName(city);

             if (seachCity == null)
                 return BadRequest("Cidade não cadastrada!");

             return seachCity;
         }

      
        [HttpPost]
        public ActionResult<City> Create(City city)
        {
            var seachCity = _serviceCity.GetName(city.Name);

            if (seachCity != null)
                return Conflict("Cidade já cadastrada!");

            _serviceCity.Create(city);

            return CreatedAtRoute("GetCity", new { cidade = city.Name }, city);
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(string id, City updateCity)
        {
            var seachCity = _serviceCity.GetId(id);

            if (seachCity == null)
                return NotFound("Cidade não está cadastrada!");

            _serviceCity.Update(id, updateCity);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var seachCity = _serviceCity.GetId(id);

            if (seachCity == null)
                return NotFound("Cidade não está cadastrada!");

            _serviceCity.Remove(id);

            return NoContent();
        }
    }
    }

