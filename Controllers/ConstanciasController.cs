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
    public class ConstanciasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConstanciasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Constancias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Constancias.Include(c => c.Empleado);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Constancias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var constancia = await _context.Constancias
                .Include(c => c.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (constancia == null)
            {
                return NotFound();
            }

            return View(constancia);
        }

        // GET: Constancias/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id");
            return View();
        }

        // POST: Constancias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmpleadoId,FechaGeneracion,Tipo,Contenido")] Constancia constancia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(constancia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", constancia.EmpleadoId);
            return View(constancia);
        }

        // GET: Constancias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var constancia = await _context.Constancias.FindAsync(id);
            if (constancia == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", constancia.EmpleadoId);
            return View(constancia);
        }

        // POST: Constancias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpleadoId,FechaGeneracion,Tipo,Contenido")] Constancia constancia)
        {
            if (id != constancia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(constancia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConstanciaExists(constancia.Id))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", constancia.EmpleadoId);
            return View(constancia);
        }

        // GET: Constancias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var constancia = await _context.Constancias
                .Include(c => c.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (constancia == null)
            {
                return NotFound();
            }

            return View(constancia);
        }

        // POST: Constancias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var constancia = await _context.Constancias.FindAsync(id);
            if (constancia != null)
            {
                _context.Constancias.Remove(constancia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConstanciaExists(int id)
        {
            return _context.Constancias.Any(e => e.Id == id);
        }
    }
}
