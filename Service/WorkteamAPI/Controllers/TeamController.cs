using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Route.Domain.Model;
using WorkteamAPI.Service;

namespace WorkteamAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TeamController : Controller
    {
       
            private readonly ServiceWorkTeam _serviceWorkTeam;

            public TeamController(ServiceWorkTeam serviceWorkTeam)
            {
                _serviceWorkTeam = serviceWorkTeam;
            }
            [HttpGet]
            public ActionResult<List<WorkTeam>> GetAll() =>
               _serviceWorkTeam.Get();

            [HttpGet("{id}")]
            public ActionResult<WorkTeam> Getid(string id)
            {
                var seachWorkTeam = _serviceWorkTeam.GetId(id);

                if (seachWorkTeam == null)
                    return BadRequest("Cadastro não encontrado!");

                return seachWorkTeam;
            }

        [HttpGet("team/{team}")]
        public ActionResult<WorkTeam> GetNameWorkTeam(string workTeam)
        {
            var seachWorkTeam = _serviceWorkTeam.GetName(workTeam);

            if (seachWorkTeam == null)
                return BadRequest("Equipe não cadastrada!");

            return seachWorkTeam;
        }


            [HttpGet("cidade/team/{cidade}", Name = "GetTeamCity")]
            public ActionResult<List<WorkTeam>> GetTeamCity(string cidade) =>
                 _serviceWorkTeam.GetCity(cidade);


            [HttpPost]
            public ActionResult<WorkTeam> Create(WorkTeam workTeam)
            {
                var seachWorkTeam = _serviceWorkTeam.GetName(workTeam.Name);
                _serviceWorkTeam.Create(workTeam);

                return CreatedAtRoute("GetEquipe", new { equipe = workTeam.Name }, workTeam);
            }

            [HttpPut("{id}")]
            public IActionResult Update(string id, WorkTeam updateWorkTeam)
            {
                var seachWorkTeam = _serviceWorkTeam.GetId(id);

                if (seachWorkTeam == null)
                    return NotFound("Equipe não está cadastrada!");

                _serviceWorkTeam.Update(id, updateWorkTeam);

                return NoContent();
            }


            [HttpDelete("{id}")]
            public IActionResult Delete(string id)
            {
                var seachWorkTeam = _serviceWorkTeam.GetId(id);

                if (seachWorkTeam == null)
                    return NotFound("Equipe não está cadastrada!");

                _serviceWorkTeam.Remove(id);

                return NoContent();
            }
        }
    }

