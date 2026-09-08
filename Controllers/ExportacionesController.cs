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
    public class ExportacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExportacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exportaciones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Exportaciones.Include(e => e.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Exportaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportaciones = await _context.Exportaciones
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exportaciones == null)
            {
                return NotFound();
            }

            return View(exportaciones);
        }

        // GET: Exportaciones/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Exportaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,NombreReporte,Formato,FechaExportacion,RutaArchivo")] Exportaciones exportaciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exportaciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", exportaciones.UsuarioId);
            return View(exportaciones);
        }

        // GET: Exportaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportaciones = await _context.Exportaciones.FindAsync(id);
            if (exportaciones == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", exportaciones.UsuarioId);
            return View(exportaciones);
        }

        // POST: Exportaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,NombreReporte,Formato,FechaExportacion,RutaArchivo")] Exportaciones exportaciones)
        {
            if (id != exportaciones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exportaciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExportacionesExists(exportaciones.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", exportaciones.UsuarioId);
            return View(exportaciones);
        }

        // GET: Exportaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportaciones = await _context.Exportaciones
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exportaciones == null)
            {
                return NotFound();
            }

            return View(exportaciones);
        }

        // POST: Exportaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exportaciones = await _context.Exportaciones.FindAsync(id);
            if (exportaciones != null)
            {
                _context.Exportaciones.Remove(exportaciones);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExportacionesExists(int id)
        {
            return _context.Exportaciones.Any(e => e.Id == id);
        }
    }
}
