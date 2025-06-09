using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOTEL_MEDITERRANEO.Data;
using HOTEL_MEDITERRANEO.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering; // Asegúrate de tener esto para SelectList

namespace HOTEL_MEDITERRANEO.Controllers
{
    public class GastosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GastosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gastos
        public async Task<IActionResult> Index()
        {
            // Obtener todos los gastos
            var gastos = await _context.Gastos.Include(g => g.Venta).ToListAsync();

            // Calcular el total de gastos
            decimal totalGastos = gastos.Sum(g => g.Monto);

            // Obtener el presupuesto (asumiendo que solo hay uno con Id = 1)
            var presupuesto = await _context.Presupuestos.FindAsync(1);
            if (presupuesto == null)
            {
                // Si no existe, créalo con un valor inicial de 0
                presupuesto = new PresupuestoModel { Id = 1, MontoTotal = 0.00m, FechaActualizacion = DateTime.Now };
                _context.Presupuestos.Add(presupuesto);
                await _context.SaveChangesAsync();
            }

            // Pasar los datos a la vista usando ViewData
            ViewData["TotalGastos"] = totalGastos;
            ViewData["MontoPresupuesto"] = presupuesto.MontoTotal;
            ViewData["PresupuestoRestante"] = presupuesto.MontoTotal - totalGastos;

            return View(gastos);
        }

        // GET: Gastos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gastoModel = await _context.Gastos
                .Include(g => g.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gastoModel == null)
            {
                return NotFound();
            }

            return View(gastoModel);
        }

        // GET: Gastos/Create
        public IActionResult Create()
        {
            ViewData["NumeroRecibo"] = new SelectList(_context.Ventas, "NumeroRecibo", "NumeroRecibo");
            return View();
        }

        // POST: Gastos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Monto,Fecha,Tipo,NumeroRecibo")] GastoModel gastoModel)
        {
            // La validación en el cliente es buena, pero el servidor también debe validar.
            // Asegúrate de que el NumeroRecibo exista si no es nulo
            if (gastoModel.NumeroRecibo != null && !_context.Ventas.Any(v => v.NumeroRecibo == gastoModel.NumeroRecibo))
            {
                ModelState.AddModelError("NumeroRecibo", "El número de recibo de venta no existe.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(gastoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NumeroRecibo"] = new SelectList(_context.Ventas, "NumeroRecibo", "NumeroRecibo", gastoModel.NumeroRecibo);
            return View(gastoModel);
        }

        // GET: Gastos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gastoModel = await _context.Gastos.FindAsync(id);
            if (gastoModel == null)
            {
                return NotFound();
            }
            ViewData["NumeroRecibo"] = new SelectList(_context.Ventas, "NumeroRecibo", "NumeroRecibo", gastoModel.NumeroRecibo);
            return View(gastoModel);
        }

        // POST: Gastos/Edit/5
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

        // GET: Gastos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gastoModel = await _context.Gastos
                .Include(g => g.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gastoModel == null)
            {
                return NotFound();
            }

            return View(gastoModel);
        }

        // POST: Gastos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gastoModel = await _context.Gastos.FindAsync(id);
            if (gastoModel != null)
            {
                _context.Gastos.Remove(gastoModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GastoModelExists(int id)
        {
            return _context.Gastos.Any(e => e.Id == id);
        }

        // ----------------------------------------------------
        // Acciones para establecer el Presupuesto
        // ----------------------------------------------------

        // GET: Gastos/SetPresupuesto
        public async Task<IActionResult> SetPresupuesto()
        {
            var presupuesto = await _context.Presupuestos.FindAsync(1);
            if (presupuesto == null)
            {
                // Si no existe, crea una instancia vacía para el formulario
                presupuesto = new PresupuestoModel { Id = 1, MontoTotal = 0.00m, FechaActualizacion = DateTime.Now };
            }
            return View(presupuesto);
        }

        // POST: Gastos/SetPresupuesto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPresupuesto([Bind("Id,MontoTotal")] PresupuestoModel presupuesto)
        {
            if (ModelState.IsValid)
            {
                var existingPresupuesto = await _context.Presupuestos.FindAsync(1);
                if (existingPresupuesto == null)
                {
                    // Esto no debería suceder si OnModelCreating inserta uno por defecto
                    _context.Add(presupuesto);
                }
                else
                {
                    // Actualiza el monto del presupuesto existente
                    existingPresupuesto.MontoTotal = presupuesto.MontoTotal;
                    existingPresupuesto.FechaActualizacion = DateTime.Now;
                    _context.Update(existingPresupuesto);
                }
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Presupuesto actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(presupuesto);
        }
    }
}