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
    public class FacturasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index(string sort, string searchName, int? pageNumber)
        {
            if (_context.Facturas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Client'  is null.");
            }

            ViewData["SearchName"] = searchName;
            ViewData["PageNumber"] = pageNumber ?? 1;


            IQueryable<Factura> facturasSql = _context.Facturas;

            if (!string.IsNullOrEmpty(searchName))
            {
                facturasSql = ((IQueryable<Factura>)_context.Facturas).Where(c => c.NumberFactura.ToString().Contains(searchName) || c.Description.ToString().Contains(searchName) || c.PriceFactura.ToString().Contains(searchName) || c.PaymentConfirme.ToString().Contains(searchName));
            }

            switch (sort)
            {
                case "number_desc":
                    facturasSql = facturasSql.OrderByDescending(x => x.NumberFactura);
                    break;
                case "number_asc":
                    facturasSql = facturasSql.OrderBy(x => x.NumberFactura);
                    break;
                case "description_desc":
                    facturasSql = facturasSql.OrderByDescending(x => x.Description);
                    break;
                case "description_asc":
                    facturasSql = facturasSql.OrderBy(x => x.Description);
                    break;

                case "pricefactura_asc":
                    facturasSql = facturasSql.OrderBy(x => x.PriceFactura);
                    break;
                case "pricefactura_desc":
                    facturasSql = facturasSql.OrderByDescending(x => x.PriceFactura);
                    break;

                case "paymentconfirme_asc":
                    facturasSql = facturasSql.OrderBy(x => x.PaymentConfirme);
                    break;
                case "paymentconfirme_desc":
                    facturasSql = facturasSql.OrderByDescending(x => x.PaymentConfirme);
                    break;
            }

            ViewData["NumberSort"] = (sort == "number_desc") ? "number_asc" : "number_desc";
            ViewData["DescriptionSort"] = (sort == "description_desc") ? "description_asc" : "description_desc";
            ViewData["PriceFacturaSort"] = (sort == "pricefactura_desc") ? "pricefactura_asc" : "pricefactura_desc";
            ViewData["PaymentConfirmeSort"] = (sort == "paymentconfirme_desc") ? "paymentconfirme_asc" : "paymentconfirme_desc";



            int pageSize = 3;

            var items = await PaginatedList<Factura>.CreateAsync(facturasSql, pageNumber ?? 1, pageSize);

            return View(items);

            var applicationDbContext = _context.Facturas.Include(f => f.Consulta);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Consulta)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            //ViewData["ConsultaID"] = new SelectList(_context.Consultas, "ID", "ID");
            ViewData["ConsultaID"] = new SelectList(_context.Consultas, "ID", "ID");
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NumberFactura,Description,PriceFactura,PaymentConfirme,ConsultaID")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsultaID"] = new SelectList(_context.Consultas, "ID", "ID", factura.ConsultaID);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewData["ConsultaID"] = new SelectList(_context.Consultas, "ID", "ID", factura.ConsultaID);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NumberFactura,Description,PriceFactura,PaymentConfirme,ConsultaID")] Factura factura)
        {
            if (id != factura.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.ID))
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
            ViewData["ConsultaID"] = new SelectList(_context.Consultas, "ID", "ID", factura.ConsultaID);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Consulta)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Facturas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Facturas'  is null.");
            }
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
          return (_context.Facturas?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
