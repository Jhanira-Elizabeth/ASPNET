using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HOTEL_MEDITERRANEO.Data;
using HOTEL_MEDITERRANEO.Models;

namespace HOTEL_MEDITERRANEO.Controllers
{
    public class GastoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GastoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gasto
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Habitaciones.Include(g => g.Venta);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Gasto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gastoModel = await _context.Habitaciones
                .Include(g => g.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gastoModel == null)
            {
                return NotFound();
            }

            return View(gastoModel);
        }

        // GET: Gasto/Create
        public IActionResult Create()
        {
            ViewData["NumeroRecibo"] = new SelectList(_context.Ventas, "NumeroRecibo", "NumeroRecibo");
            return View();
        }

        // POST: Gasto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Monto,Fecha,Tipo,NumeroRecibo")] GastoModel gastoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gastoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NumeroRecibo"] = new SelectList(_context.Ventas, "NumeroRecibo", "NumeroRecibo", gastoModel.NumeroRecibo);
            return View(gastoModel);
        }

        // GET: Gasto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gastoModel = await _context.Habitaciones.FindAsync(id);
            if (gastoModel == null)
            {
                return NotFound();
            }
            ViewData["NumeroRecibo"] = new SelectList(_context.Ventas, "NumeroRecibo", "NumeroRecibo", gastoModel.NumeroRecibo);
            return View(gastoModel);
        }

        // POST: Gasto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Monto,Fecha,Tipo,NumeroRecibo")] GastoModel gastoModel)
        {
            if (id != gastoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gastoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GastoModelExists(gastoModel.Id))
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
            ViewData["NumeroRecibo"] = new SelectList(_context.Ventas, "NumeroRecibo", "NumeroRecibo", gastoModel.NumeroRecibo);
            return View(gastoModel);
        }

        // GET: Gasto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gastoModel = await _context.Habitaciones
                .Include(g => g.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gastoModel == null)
            {
                return NotFound();
            }

            return View(gastoModel);
        }

        // POST: Gasto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gastoModel = await _context.Habitaciones.FindAsync(id);
            if (gastoModel != null)
            {
                _context.Habitaciones.Remove(gastoModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GastoModelExists(int id)
        {
            return _context.Habitaciones.Any(e => e.Id == id);
        }
    }
}
