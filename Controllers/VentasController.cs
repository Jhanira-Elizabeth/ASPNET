using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HOTEL_MEDITERRANEO.Data;
using HOTEL_MEDITERRANEO.Models;
//Nuevo using
using Rotativa.AspNetCore;

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

        // Nueva acción para generar el PDF
        [HttpGet] // Puedes hacerlo con un botón que apunte a esta URL
        public async Task<IActionResult> DescargarComprobantesPdf(
            DateTime? fechaConsulta, // Para el filtro por fecha específica
            int? mesConsulta,       // Para el filtro por mes
            int? anioConsulta)      // Para el filtro por año
        {
            IQueryable<VentasModel> ventasQuery = _context.Ventas;

            if (fechaConsulta.HasValue)
            {
                ventasQuery = ventasQuery.Where(v => v.Fecha.Date == fechaConsulta.Value.Date);
            }
            else if (mesConsulta.HasValue && anioConsulta.HasValue)
            {
                ventasQuery = ventasQuery.Where(v => v.Fecha.Month == mesConsulta.Value && v.Fecha.Year == anioConsulta.Value);
            }
            // Si no se especifica ningún filtro, se descargan todas las ventas (comportamiento actual)

            var ventas = await ventasQuery.ToListAsync();

            return new ViewAsPdf("VentasPdf", ventas) // Sigue usando la vista VentasPdf existente
            {
                FileName = "ComprobantesDeVentas.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                // Puedes personalizar el nombre del archivo si hay filtros
                // FileName = $"Reporte_Ventas_{(fechaConsulta.HasValue ? fechaConsulta.Value.ToString("yyyyMMdd") : (mesConsulta.HasValue ? $"{mesConsulta.Value:00}{anioConsulta.Value}" : "General"))}.pdf"
            };
        }


        // NUEVA ACCIÓN: Para generar el PDF de un comprobante individual
        [HttpGet]
        public async Task<IActionResult> DescargarComprobanteIndividualPdf(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .FirstOrDefaultAsync(m => m.NumeroRecibo == id);

            if (venta == null)
            {
                return NotFound();
            }

            // Usaremos una nueva vista específica para el comprobante individual
            return new ViewAsPdf("ComprobanteIndividualPdf", venta)
            {
                FileName = $"Comprobante_{venta.NumeroRecibo}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A5 // A5 es más pequeño, ideal para un solo recibo
            };
        }
    }
}
