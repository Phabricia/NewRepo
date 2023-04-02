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
    public class ConsultasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consultas
        public async Task<IActionResult> Index(string sort, string searchName, int? pageNumber)
        {
            if (_context.Consultas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Client'  is null.");
            }

            ViewData["SearchName"] = searchName;
            ViewData["PageNumber"] = pageNumber ?? 1;


            IQueryable<Consulta> consultasSql = _context.Consultas.Include(c => c.Client).Include(c => c.Team);

            if (!string.IsNullOrEmpty(searchName))
            {
                consultasSql = ((IQueryable<Consulta>)_context.Consultas).Where(c => c.NumberConsulta.ToString().Contains(searchName) || c.DayTime.ToString().Contains(searchName));
            }

            switch (sort)
             {
                    case "number_desc":
                        consultasSql = consultasSql.OrderByDescending(x => x.NumberConsulta);
                        break;
                    case "number_asc":
                        consultasSql = consultasSql.OrderBy(x => x.NumberConsulta);
                        break;
                    case "daytimedesc":
                        consultasSql = consultasSql.OrderByDescending(x => x.DayTime);
                        break;
                    case "daytime_asc":
                        consultasSql = consultasSql.OrderBy(x => x.DayTime);
                        break;

                    case "affirmative_asc":
                        consultasSql = consultasSql.OrderBy(x => x.Affirmative);
                        break;
                    case "affirmative_desc":
                        consultasSql = consultasSql.OrderByDescending(x => x.Affirmative);
                        break;

                    case "observation_asc":
                        consultasSql = consultasSql.OrderBy(x => x.Observation);
                        break;
                    case "observation_desc":
                        consultasSql = consultasSql.OrderByDescending(x => x.Observation);
                        break;
             }

            ViewData["NumberSort"] =  (sort == "number_desc") ? "number_asc" : "number_desc";
            ViewData["DayTimeSort"] = (sort == "daytime_desc") ? "daytime_asc" : "daytime_desc";
            ViewData["AffirmativeSort"] = (sort == "affirmative_desc") ? "affirmative_asc" : "affirmative_desc";
            ViewData["ObservationSort"] = (sort == "observation_desc") ? "observation_asc" : "observation_desc";



                int pageSize = 3;

                var items = await PaginatedList<Consulta>.CreateAsync(consultasSql, pageNumber ?? 1, pageSize);

                return View(items);

            var applicationDbContext = _context.Consultas.Include(c => c.Client).Include(c => c.Team);
            return View(await applicationDbContext.ToListAsync());

        }

       

        // GET: Consultas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consultas == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.Client)
                .Include(c => c.Team)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // GET: Consultas/Create
        public IActionResult Create()
        {
            //ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "ID");
            //ViewData["Client"] = new SelectList(_context.Clients, dataValueField: "ID", dataTextField: "Name");
            ViewData["ClientID"] = new SelectList(_context.Clients, dataValueField: "ID", dataTextField: "Name");
            ViewData["TeamID"] = new SelectList(_context.Teams, dataValueField: "ID", dataTextField: "Name");
            //ViewData["TeamID"] = new SelectList(_context.Teams, dataValueField: "ID", dataTextField: "Name");
            return View();
        }

        // POST: Consultas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NumberConsulta,DayTime,Affirmative,Observation,ClientID,TeamID")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "ID", consulta.ClientID);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "ID", consulta.TeamID);
            return View(consulta);
        }

        // GET: Consultas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consultas == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }
            //ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "ID", consulta.ClientID);
            ViewData["ClientID"] = new SelectList(_context.Clients, dataValueField: "ID", dataTextField: "Name", consulta.ClientID);
            ViewData["TeamID"] = new SelectList(_context.Teams, dataValueField: "ID", dataTextField: "Name", consulta.TeamID);
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NumberConsulta,DayTime,Affirmative,Observation,ClientID,TeamID")] Consulta consulta)
        {
            if (id != consulta.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaExists(consulta.ID))
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
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "ID", consulta.ClientID);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "ID", consulta.TeamID);
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consultas == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.Client)
                .Include(c => c.Team)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consultas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Consultas'  is null.");
            }
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaExists(int id)
        {
          return (_context.Consultas?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
