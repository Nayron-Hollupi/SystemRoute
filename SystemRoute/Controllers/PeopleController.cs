using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Route.Domain.Model;
using Serviceapplication.ServicePerson;
using SystemRoute.Data;

namespace SystemRoute.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(await ServicePersonApp.GetPerson());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await ServicePersonApp.SeachPerson(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Status,CreatedAt,LastUpdateDate,User,Id")] Person person)
        {

            person.Status = true;
            person.User = User.Identity.Name;
            if (ModelState.IsValid)
            {
                ServicePersonApp.PostPerson(person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
       
          var person= await ServicePersonApp.SeachPerson(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Status,CreatedAt,LastUpdateDate,User,Id")] Person person)
        {

           person.CreatedAt = person.CreatedAt;
            person.LastUpdateDate = DateTime.Now;
            person.User = User.Identity.Name;
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    person.Id = id;
                    var seachPerson = await ServicePersonApp.SeachPerson(id);
                    ServicePersonApp.UpdatePerson(id, person);
                }
                catch (DbUpdateConcurrencyException)
                {
                     if (!PersonExists(person.Id))
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
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var person = await ServicePersonApp.SeachPerson(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var person = await ServicePersonApp.SeachPerson(id);
            ServicePersonApp.DeletePerson(id);
 
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(string id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
