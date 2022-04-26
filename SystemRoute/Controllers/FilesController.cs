using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Route.Domain.Model;
using Serviceapplication.ServiceExcel;
using SystemRoute.Data;

namespace SystemRoute.Controllers
{
    public class FilesController : Controller
    {
         private readonly ApplicationDbContext _context;

          public FilesController(ApplicationDbContext context)
          {
              _context = context;
          }

          // GET: Files
          public IActionResult Index(IFormFile file)
          {
              var retorno = FileExcel.ReadExcelXlsx(file);

                ViewBag.ReadFile = retorno;
              return View();
              // return View(await _context.File.ToListAsync());
          }

          // GET: Files/Details/5
          public async Task<IActionResult> Details(string id)
          {
              if (id == null)
              {
                  return NotFound();
              }

              var file = await _context.File
                  .FirstOrDefaultAsync(m => m.Id == id);
              if (file == null)
              {
                  return NotFound();
              }

              return View(file);
          }

          // GET: Files/Create
          public IActionResult Create()
          {
              return View();
          }

          // POST: Files/Create
          // To protect from overposting attacks, enable the specific properties you want to bind to.
          // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Create([Bind("Description,FileDescription,Id")] File file)
          {
              if (ModelState.IsValid)
              {
                  _context.Add(file);
                  await _context.SaveChangesAsync();
                  return RedirectToAction(nameof(Index));
              }
              return View(file);
          }

          // GET: Files/Edit/5
          public async Task<IActionResult> Edit(string id)
          {
              if (id == null)
              {
                  return NotFound();
              }

              var file = await _context.File.FindAsync(id);
              if (file == null)
              {
                  return NotFound();
              }
              return View(file);
          }

          // POST: Files/Edit/5
          // To protect from overposting attacks, enable the specific properties you want to bind to.
          // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Edit(string id, [Bind("Description,FileDescription,Id")] File file)
          {
              if (id != file.Id)
              {
                  return NotFound();
              }

              if (ModelState.IsValid)
              {
                  try
                  {
                      _context.Update(file);
                      await _context.SaveChangesAsync();
                  }
                  catch (DbUpdateConcurrencyException)
                  {
                      if (!FileExists(file.Id))
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
              return View(file);
          }

          // GET: Files/Delete/5
          public async Task<IActionResult> Delete(string id)
          {
              if (id == null)
              {
                  return NotFound();
              }

              var file = await _context.File
                  .FirstOrDefaultAsync(m => m.Id == id);
              if (file == null)
              {
                  return NotFound();
              }

              return View(file);
          }

          // POST: Files/Delete/5
          [HttpPost, ActionName("Delete")]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> DeleteConfirmed(string id)
          {
              var file = await _context.File.FindAsync(id);
              _context.File.Remove(file);
              await _context.SaveChangesAsync();
              return RedirectToAction(nameof(Index));
          }

          private bool FileExists(string id)
          {
              return _context.File.Any(e => e.Id == id);
          }
    }
}
