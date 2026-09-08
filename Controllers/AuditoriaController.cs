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
    public class AuditoriaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuditoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Auditoria
        public async Task<IActionResult> Index()
        {
            return View(await _context.Auditorias.ToListAsync());
        }

        // GET: Auditoria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditoria = await _context.Auditorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auditoria == null)
            {
                return NotFound();
            }

            return View(auditoria);
        }

        // GET: Auditoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Auditoria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Usuario,Modulo,Accion,ValorAnterior,ValorNuevo,Fecha")] Auditoria auditoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auditoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(auditoria);
        }

        // GET: Auditoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditoria = await _context.Auditorias.FindAsync(id);
            if (auditoria == null)
            {
                return NotFound();
            }
            return View(auditoria);
        }

        // POST: Auditoria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Usuario,Modulo,Accion,ValorAnterior,ValorNuevo,Fecha")] Auditoria auditoria)
        {
            if (id != auditoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auditoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuditoriaExists(auditoria.Id))
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
            return View(auditoria);
        }

        // GET: Auditoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditoria = await _context.Auditorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auditoria == null)
            {
                return NotFound();
            }

            return View(auditoria);
        }

        // POST: Auditoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auditoria = await _context.Auditorias.FindAsync(id);
            if (auditoria != null)
            {
                _context.Auditorias.Remove(auditoria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuditoriaExists(int id)
        {
            return _context.Auditorias.Any(e => e.Id == id);
        }
    }
}
