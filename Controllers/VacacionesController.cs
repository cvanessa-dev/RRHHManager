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
    public class VacacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VacacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vacaciones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vacaciones.Include(v => v.Empleado);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vacaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacaciones = await _context.Vacaciones
                .Include(v => v.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacaciones == null)
            {
                return NotFound();
            }

            return View(vacaciones);
        }

        // GET: Vacaciones/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id");
            return View();
        }

        // POST: Vacaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmpleadoId,FechaInicio,FechaFin,Estado")] Vacaciones vacaciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacaciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", vacaciones.EmpleadoId);
            return View(vacaciones);
        }

        // GET: Vacaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacaciones = await _context.Vacaciones.FindAsync(id);
            if (vacaciones == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", vacaciones.EmpleadoId);
            return View(vacaciones);
        }

        // POST: Vacaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpleadoId,FechaInicio,FechaFin,Estado")] Vacaciones vacaciones)
        {
            if (id != vacaciones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacaciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacacionesExists(vacaciones.Id))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", vacaciones.EmpleadoId);
            return View(vacaciones);
        }

        // GET: Vacaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacaciones = await _context.Vacaciones
                .Include(v => v.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacaciones == null)
            {
                return NotFound();
            }

            return View(vacaciones);
        }

        // POST: Vacaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacaciones = await _context.Vacaciones.FindAsync(id);
            if (vacaciones != null)
            {
                _context.Vacaciones.Remove(vacaciones);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacacionesExists(int id)
        {
            return _context.Vacaciones.Any(e => e.Id == id);
        }
    }
}
