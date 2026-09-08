using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RRHHManager.Data;
using RRHHManager.Models;

namespace RRHHManager.Controllers
{
    public class HistorialSalarialController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistorialSalarialController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HistorialSalarial
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HistorialSalarials.Include(h => h.Empleado);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HistorialSalarial/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialSalarial = await _context.HistorialSalarials
                .Include(h => h.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historialSalarial == null)
            {
                return NotFound();
            }

            return View(historialSalarial);
        }

        // GET: HistorialSalarial/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id");
            return View();
        }

        // POST: HistorialSalarial/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmpleadoId,SalarioAnterior,SalarioNuevo,PorcentajeAumento,FechaCambio,Motivo")] HistorialSalarial historialSalarial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historialSalarial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", historialSalarial.EmpleadoId);
            return View(historialSalarial);
        }

        // GET: HistorialSalarial/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialSalarial = await _context.HistorialSalarials.FindAsync(id);
            if (historialSalarial == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", historialSalarial.EmpleadoId);
            return View(historialSalarial);
        }

        // POST: HistorialSalarial/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpleadoId,SalarioAnterior,SalarioNuevo,PorcentajeAumento,FechaCambio,Motivo")] HistorialSalarial historialSalarial)
        {
            if (id != historialSalarial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialSalarial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialSalarialExists(historialSalarial.Id))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", historialSalarial.EmpleadoId);
            return View(historialSalarial);
        }

        // GET: HistorialSalarial/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialSalarial = await _context.HistorialSalarials
                .Include(h => h.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historialSalarial == null)
            {
                return NotFound();
            }

            return View(historialSalarial);
        }

        // POST: HistorialSalarial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historialSalarial = await _context.HistorialSalarials.FindAsync(id);
            if (historialSalarial != null)
            {
                _context.HistorialSalarials.Remove(historialSalarial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialSalarialExists(int id)
        {
            return _context.HistorialSalarials.Any(e => e.Id == id);
        }
    }
}
