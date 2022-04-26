using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Route.Domain.Model;
using RouteSystem.Data;
using Serviceapplication.ServicePerson;
using Serviceapplication.ServiceWorkTeam;

namespace RouteSystem.Controllers
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
        public async Task<IActionResult> Create([Bind("Name,Person,City,CreatedAt,LastUpdateDate,User,Id")] WorkTeam workTeam)
        {
          
            string line;
            int i = 0, peoplelimit = 0;
            var people = "";

            line = Request.Form["Person"].ToString();

            var values = line.Split(',');

            for (; i < values.Length; i++)
            {

                var person = await ServicePersonApp.GetPersonName(values[i].TrimStart(' ').TrimEnd(' '));


                if (peoplelimit == 0 && person != null && person.Status == false)
                {
                    people = (values[i]);
                    peoplelimit++;
                    person.Status = true;
                   ServicePersonApp.UpdatePerson(person.Id, person);

                }
                else if (i != 0 && person != null && person.Status == false)
                {
                    people = (people + "," + values[i]);
                    person.Status = true;
                    ServicePersonApp.UpdatePerson(person.Id, person);
                }

            }

            string peopleTeam = people.ToString();
            workTeam.Person = peopleTeam;
      

            if (peoplelimit == 0 || peoplelimit > 5)
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


        

            ViewBag.PersonTeam = workTeam;
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
        public async Task<IActionResult> Edit(string id, [Bind("Name,Person,City,CreatedAt,LastUpdateDate,User,Id")] WorkTeam workTeam)
        {
            if (id != workTeam.Id)
            {
                return NotFound();
            }
            var peoples = workTeam.Person;
            string line;
            int i = 0, peoplelimit = 0;
            var people = "";

            line = Request.Form["Person"].ToString();

            var values = line.Split(',');

            for (; i < values.Length; i++)
            {

                var person = await ServicePersonApp.GetPersonName(values[i].TrimStart(' ').TrimEnd(' '));


                if (peoplelimit == 0 && person != null && person.Status == false)
                {
                    people = (values[i]);
                    peoplelimit++;
                    person.Status = true;
                    ServicePersonApp.UpdatePerson(person.Id, person);

                }
                else if (i != 0 && person != null && person.Status == false)
                {
                    people = (people + "," + values[i]);
                    person.Status = true;
                    ServicePersonApp.UpdatePerson(person.Id, person);
                }

            }

            string peopleTeam = people.ToString();
            workTeam.Person = peopleTeam;
          

            if (peoplelimit == 0 || peoplelimit > 5)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(workTeam);
                        await _context.SaveChangesAsync();
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

            var workTeam = await _context.WorkTeam
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var workTeam = await _context.WorkTeam.FindAsync(id);
            _context.WorkTeam.Remove(workTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkTeamExists(string id)
        {
            return _context.WorkTeam.Any(e => e.Id == id);
        }
    }
}
