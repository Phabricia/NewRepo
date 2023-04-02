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
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index(string sort, string searchName, int? pageNumber)
        {
            if (_context.Teams == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Client'  is null.");
            }

            ViewData["SearchName"] = searchName;
            ViewData["PageNumber"] = pageNumber ?? 1;


            IQueryable<Team> teamsSql = _context.Teams;

            if (!string.IsNullOrEmpty(searchName))
            {
                teamsSql = ((IQueryable<Team>)_context.Teams).Where(c => c.Name.ToString().Contains(searchName) || c.Birthday.ToString().Contains(searchName) || c.Address.ToString().Contains(searchName) || c.NIF.ToString().Contains(searchName) || c.Position.ToString().Contains(searchName) || c.Expertise.ToString().Contains(searchName));
            }

            switch (sort)
            {
                case "name_desc":
                    teamsSql = teamsSql.OrderByDescending(x => x.Name);
                    break;
                case "name_asc":
                    teamsSql = teamsSql.OrderBy(x => x.Name);
                    break;
                case "birthday_desc":
                    teamsSql = teamsSql.OrderByDescending(x => x.Birthday);
                    break;
                case "birthday_asc":
                    teamsSql = teamsSql.OrderBy(x => x.Birthday);
                    break;

                case "address_asc":
                    teamsSql = teamsSql.OrderBy(x => x.Address);
                    break;
                case "address_desc":
                    teamsSql = teamsSql.OrderByDescending(x => x.Address);
                    break;

                case "nif_asc":
                    teamsSql = teamsSql.OrderBy(x => x.NIF);
                    break;
                case "nif_desc":
                    teamsSql = teamsSql.OrderByDescending(x => x.NIF);
                    break;
                case "position_asc":
                    teamsSql = teamsSql.OrderBy(x => x.Position);
                    break;
                case "position_desc":
                    teamsSql = teamsSql.OrderByDescending(x => x.Position);
                    break;
                case "expertise_asc":
                    teamsSql = teamsSql.OrderBy(x => x.Expertise);
                    break;
                case "expertise_desc":
                    teamsSql = teamsSql.OrderByDescending(x => x.Expertise);
                    break;
            }

            ViewData["NameSort"] = (sort == "name_desc") ? "name_asc" : "name_desc";
            ViewData["BirthdaySort"] = (sort == "birthday_desc") ? "birthday_asc" : "birthday_desc";
            ViewData["AddressSort"] = (sort == "address_desc") ? "address_asc" : "address_desc";
            ViewData["NIFSort"] = (sort == "nif_desc") ? "nif_asc" : "nif_desc";
            ViewData["PositionSort"] = (sort == "position_desc") ? "position_asc" : "position_desc";
            ViewData["ExpertiseSort"] = (sort == "expertise_desc") ? "expertise_asc" : "expertise_desc";



            int pageSize = 3;

            var items = await PaginatedList<Team>.CreateAsync(teamsSql, pageNumber ?? 1, pageSize);

            return View(items);


            return _context.Teams != null ? 
                          View(await _context.Teams.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Teams'  is null.");
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.ID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Birthday,Address,NIF,Position,Expertise")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Birthday,Address,NIF,Position,Expertise")] Team team)
        {
            if (id != team.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.ID))
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
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.ID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teams == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Teams'  is null.");
            }
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
          return (_context.Teams?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
