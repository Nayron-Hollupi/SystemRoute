using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Route.Domain.Model;
using Serviceapplication.ServicePerson;
using Serviceapplication.ServiceWorkTeam;
using SystemRoute.Data;

namespace SystemRoute.Controllers
{
    public class WorkTeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkTeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkTeams
        public async Task<IActionResult> Index()
        {
            return View(await ServiceTeamApp.GetWorkTeam());
        }

        // GET: WorkTeams/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var workTeam = await ServiceTeamApp.SeachWorkTeam(id);
            if (workTeam == null)
            {
                return NotFound();
            }

            return View(workTeam);
        }

        // GET: WorkTeams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Person,CreatedAt,LastUpdateDate,User,Id")] WorkTeam workTeam)
        {
            string line;
            int i = 0, p = 0;
            var people = "";
           
            line = workTeam.Person;

            var values = line.Split(',');

            for (; i < values.Length; i++)
            {

               var person = await ServicePersonApp.GetPersonName(values[i].TrimStart(' ').TrimEnd(' '));


              if (p == 0 && person != null && person.Status == false)
                {
                    people = (values[i]);
                    p++;
                     person.Status = true;
                     ServicePersonApp.UpdatePerson(person.Id, person);

                }
                else if (i != 0 && person != null && person.Status == false)
                {
                    people = (people + "," + values[i]);
                    person.Status = true;
                    ServicePersonApp.UpdatePerson(person.Id,  person);
                }
            
            }

            string peopleTeam = people.ToString();
            workTeam.Person = peopleTeam;
            workTeam.User = User.Identity.Name;

            if(p == 0)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                ServiceTeamApp.PostWorkTeam(workTeam);
                return RedirectToAction(nameof(Index));
            }
            return View(workTeam);
        }

        // GET: WorkTeams/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var workTeam = await ServiceTeamApp.SeachWorkTeam(id);


           
            if (workTeam == null)
            {
                return NotFound();
            }
            return View(workTeam);
        }

        // POST: WorkTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Person,CreatedAt,LastUpdateDate,User,Id")] WorkTeam workTeam)
        {
            if (id != workTeam.Id)
            {
                return NotFound();
            }


            workTeam.User = User.Identity.Name;
            workTeam.LastUpdateDate = DateTime.Now;
            workTeam.CreatedAt = workTeam.CreatedAt;
            if (ModelState.IsValid)
            {
                try
                {
                    var seachTeam = await ServiceTeamApp.SeachWorkTeam(id);
                   ServiceTeamApp.UpdateWorkTeam(id, workTeam);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkTeamExists(workTeam.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(workTeam);
        }

        // GET: WorkTeams/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTeam = await ServiceTeamApp.SeachWorkTeam(id);
            if (workTeam == null)
            {
                return NotFound();
            }

            return View(workTeam);
        }

        // POST: WorkTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var workTeam = await ServiceTeamApp.SeachWorkTeam(id);
            ServiceTeamApp.DeleteWorkTeam(id);

            return RedirectToAction(nameof(Index));
        }

        private bool WorkTeamExists(string id)
        {
            return _context.WorkTeam.Any(e => e.Id == id);
        }
    }
}
