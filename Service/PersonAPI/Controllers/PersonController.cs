using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PersonAPI.Service;
using Route.Domain.Model;

namespace PersonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly ServicePerson _servicePerson;

        public PersonController(ServicePerson servicePerson)
        {
            _servicePerson = servicePerson;
        }
        [HttpGet]
        public ActionResult<List<Person>> GetAll() =>
           _servicePerson.Get();

        [HttpGet("{id}")]
        public ActionResult<Person> Getid(string id)
        {
            var seachPerson = _servicePerson.GetId(id);

            if (seachPerson == null)
                return BadRequest("Cadastro não encontrado!");

            return seachPerson;
        }

        [HttpGet("person/{person}")]
        public ActionResult<Person> GetNameCity(string person)
        {
            var seachPerson = _servicePerson.GetName(person);

            if (seachPerson == null)
                return BadRequest("Pessoa não cadastrada!");

            return seachPerson;
        }



        [HttpGet("/status")]
        public ActionResult<List<Person>> GetStatus() =>
            _servicePerson.GetStatus();

        [HttpPost]
        public ActionResult<Person> Create(Person person)
        {
            var seachPerson = _servicePerson.GetName(person.Name);
            _servicePerson.Create(person);

            return CreatedAtRoute("GetPerson", new { pessoa = person.Name }, person);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Person updatePerson)
        {
            var seachPerson = _servicePerson.GetId(id);

            if (seachPerson == null)
                return NotFound("Pessoa não está cadastrada!");

            _servicePerson.Update(id, updatePerson);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var seachPerson = _servicePerson.GetId(id);

            if (seachPerson == null)
                return NotFound("Pessoa não está cadastrada!");

            _servicePerson.Remove(id);

            return NoContent();
        }
    }
}

