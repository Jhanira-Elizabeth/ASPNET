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
    public class VentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ventas.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventasModel = await _context.Ventas
                .FirstOrDefaultAsync(m => m.NumeroRecibo == id);
            if (ventasModel == null)
            {
                return NotFound();
            }

            return View(ventasModel);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroRecibo,NombreCliente,Monto,MetodoPago,Fecha,NumeroHabitacion,NumeroPersonas")] VentasModel ventasModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventasModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ventasModel);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventasModel = await _context.Ventas.FindAsync(id);
            if (ventasModel == null)
            {
                return NotFound();
            }
            return View(ventasModel);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumeroRecibo,NombreCliente,Monto,MetodoPago,Fecha,NumeroHabitacion,NumeroPersonas")] VentasModel ventasModel)
        {
            if (id != ventasModel.NumeroRecibo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventasModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentasModelExists(ventasModel.NumeroRecibo))
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
            return View(ventasModel);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventasModel = await _context.Ventas
                .FirstOrDefaultAsync(m => m.NumeroRecibo == id);
            if (ventasModel == null)
            {
                return NotFound();
            }

            return View(ventasModel);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ventasModel = await _context.Ventas.FindAsync(id);
            if (ventasModel != null)
            {
                _context.Ventas.Remove(ventasModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentasModelExists(string id)
        {
            return _context.Ventas.Any(e => e.NumeroRecibo == id);
        }
    }
}
