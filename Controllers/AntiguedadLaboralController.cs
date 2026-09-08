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
    public class AntiguedadLaboralController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AntiguedadLaboralController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AntiguedadLaboral
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AntiguedadLaborals.Include(a => a.Empleado);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AntiguedadLaboral/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var antiguedadLaboral = await _context.AntiguedadLaborals
                .Include(a => a.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (antiguedadLaboral == null)
            {
                return NotFound();
            }

            return View(antiguedadLaboral);
        }

        // GET: AntiguedadLaboral/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id");
            return View();
        }

        // POST: AntiguedadLaboral/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmpleadoId,FechaIngreso,FechaCalculo,Anios,Meses,Dias")] AntiguedadLaboral antiguedadLaboral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(antiguedadLaboral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", antiguedadLaboral.EmpleadoId);
            return View(antiguedadLaboral);
        }

        // GET: AntiguedadLaboral/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var antiguedadLaboral = await _context.AntiguedadLaborals.FindAsync(id);
            if (antiguedadLaboral == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", antiguedadLaboral.EmpleadoId);
            return View(antiguedadLaboral);
        }

        // POST: AntiguedadLaboral/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpleadoId,FechaIngreso,FechaCalculo,Anios,Meses,Dias")] AntiguedadLaboral antiguedadLaboral)
        {
            if (id != antiguedadLaboral.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(antiguedadLaboral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AntiguedadLaboralExists(antiguedadLaboral.Id))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", antiguedadLaboral.EmpleadoId);
            return View(antiguedadLaboral);
        }

        // GET: AntiguedadLaboral/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var antiguedadLaboral = await _context.AntiguedadLaborals
                .Include(a => a.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (antiguedadLaboral == null)
            {
                return NotFound();
            }

            return View(antiguedadLaboral);
        }

        // POST: AntiguedadLaboral/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var antiguedadLaboral = await _context.AntiguedadLaborals.FindAsync(id);
            if (antiguedadLaboral != null)
            {
                _context.AntiguedadLaborals.Remove(antiguedadLaboral);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AntiguedadLaboralExists(int id)
        {
            return _context.AntiguedadLaborals.Any(e => e.Id == id);
        }
    }
}
