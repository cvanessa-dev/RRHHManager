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
    public class ExpedientesDigitalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpedientesDigitalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExpedientesDigitales
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.expedientesDigitals.Include(e => e.Empleado);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ExpedientesDigitales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expedientesDigitales = await _context.expedientesDigitals
                .Include(e => e.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expedientesDigitales == null)
            {
                return NotFound();
            }

            return View(expedientesDigitales);
        }

        // GET: ExpedientesDigitales/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id");
            return View();
        }

        // POST: ExpedientesDigitales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmpleadoId,TipoDocumento,NombreArchivo,RutaArchivo,FechaCarga,CargadoPor,Eliminado")] ExpedientesDigitales expedientesDigitales)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expedientesDigitales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", expedientesDigitales.EmpleadoId);
            return View(expedientesDigitales);
        }

        // GET: ExpedientesDigitales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expedientesDigitales = await _context.expedientesDigitals.FindAsync(id);
            if (expedientesDigitales == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", expedientesDigitales.EmpleadoId);
            return View(expedientesDigitales);
        }

        // POST: ExpedientesDigitales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpleadoId,TipoDocumento,NombreArchivo,RutaArchivo,FechaCarga,CargadoPor,Eliminado")] ExpedientesDigitales expedientesDigitales)
        {
            if (id != expedientesDigitales.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expedientesDigitales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpedientesDigitalesExists(expedientesDigitales.Id))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", expedientesDigitales.EmpleadoId);
            return View(expedientesDigitales);
        }

        // GET: ExpedientesDigitales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expedientesDigitales = await _context.expedientesDigitals
                .Include(e => e.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expedientesDigitales == null)
            {
                return NotFound();
            }

            return View(expedientesDigitales);
        }

        // POST: ExpedientesDigitales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expedientesDigitales = await _context.expedientesDigitals.FindAsync(id);
            if (expedientesDigitales != null)
            {
                _context.expedientesDigitals.Remove(expedientesDigitales);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpedientesDigitalesExists(int id)
        {
            return _context.expedientesDigitals.Any(e => e.Id == id);
        }
    }
}
