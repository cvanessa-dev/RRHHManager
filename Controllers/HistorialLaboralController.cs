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
    public class HistorialLaboralController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistorialLaboralController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HistorialLaboral
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HistorialLaborals.Include(h => h.Empleado);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HistorialLaboral/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialLaboral = await _context.HistorialLaborals
                .Include(h => h.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historialLaboral == null)
            {
                return NotFound();
            }

            return View(historialLaboral);
        }

        // GET: HistorialLaboral/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id");
            return View();
        }

        // POST: HistorialLaboral/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmpleadoId,FechaMovimiento,TipoMovimiento,Motivo,Observaciones")] HistorialLaboral historialLaboral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historialLaboral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", historialLaboral.EmpleadoId);
            return View(historialLaboral);
        }

        // GET: HistorialLaboral/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialLaboral = await _context.HistorialLaborals.FindAsync(id);
            if (historialLaboral == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", historialLaboral.EmpleadoId);
            return View(historialLaboral);
        }

        // POST: HistorialLaboral/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpleadoId,FechaMovimiento,TipoMovimiento,Motivo,Observaciones")] HistorialLaboral historialLaboral)
        {
            if (id != historialLaboral.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialLaboral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialLaboralExists(historialLaboral.Id))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", historialLaboral.EmpleadoId);
            return View(historialLaboral);
        }

        // GET: HistorialLaboral/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialLaboral = await _context.HistorialLaborals
                .Include(h => h.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historialLaboral == null)
            {
                return NotFound();
            }

            return View(historialLaboral);
        }

        // POST: HistorialLaboral/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historialLaboral = await _context.HistorialLaborals.FindAsync(id);
            if (historialLaboral != null)
            {
                _context.HistorialLaborals.Remove(historialLaboral);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialLaboralExists(int id)
        {
            return _context.HistorialLaborals.Any(e => e.Id == id);
        }
    }
}
