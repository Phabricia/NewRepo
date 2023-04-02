using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaDentariaProjeto.Data;
using ClinicaDentariaProjeto.Models;
using Microsoft.AspNetCore.Authorization;
using ClinicaDentariaProjeto.lib;

namespace ClinicaDentariaProjeto.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string sort, string searchName, int? pageNumber)
        {
            ViewData["SearchName"] = searchName;

            var clientSql = from i in _context.Clients select i;

            if (!string.IsNullOrEmpty(searchName))
            {
                clientSql=clientSql.Where(i => i.Name.Contains(searchName));
            }

            switch (sort)
            {
                case "name_desc":
                    clientSql = clientSql.OrderByDescending(x => x.Name);
                    break;
                case "name_asc":
                    clientSql = clientSql.OrderBy(x => x.Name);
                    break;
                case "birthday_desc":
                    clientSql = clientSql.OrderByDescending(x => x.Birthday);
                    break;
                case "birthday_asc":
                    clientSql = clientSql.OrderBy(x => x.Birthday);
                    break;

                case "address_asc":
                    clientSql = clientSql.OrderBy(x => x.Address);
                    break;
                case "address_desc":
                    clientSql = clientSql.OrderByDescending(x => x.Address);
                    break;

                case "healthcare_asc":
                    clientSql = clientSql.OrderBy(x => x.HealthCare);
                    break;
                case "healthcare_desc":
                    clientSql = clientSql.OrderByDescending(x => x.HealthCare);
                    break;
            }

            if (sort == "name_desc")
            {
                ViewData["NameSort"] = "name_asc";
            }
            else
            {
                ViewData["NameSort"] = "name_desc";
            }

            if (sort == "birthday_desc")
            {
                ViewData["BirthdaySort"] = "birthday_asc";
            }
            else
            {
                ViewData["BirthdaySort"] = "birthday_desc";
            }

            if (sort == "address_desc")
            {
                ViewData["AddressSort"] = "address_asc";
            }
            else
            {
                ViewData["AddressSort"] = "address_desc";
            }

            if (sort == "healthcare_desc")
            {
                ViewData["HealthCareSort"] = "healthcare_asc";
            }
            else
            {
                ViewData["HealthCareSort"] = "healthcare_desc";
            }

            int pageSize = 5;

            var items = await PaginatedList<Client>.CreateAsync(clientSql, pageNumber ?? 1, pageSize);

            return View(items);
        

              return _context.Clients != null ? 
                          View(await _context.Clients.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Clients'  is null.");
     
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Birthday,Address,NIF,HealthCare")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Birthday,Address,NIF,HealthCare")] Client client)
        {
            if (id != client.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ID))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Clients'  is null.");
            }
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return (_context.Clients?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
