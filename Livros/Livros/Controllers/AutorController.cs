using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livros.Models;
using Livros.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Livros.Controllers
{
    public class AutorController : Controller
    {
        private readonly LivrosContext _context;
        public AutorController(LivrosContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Autores.OrderBy(c => c.Name).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Nacionalidade")] Autor autor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(autor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados.");
            }
            return View(autor);
        }
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var autor = await _context.Autores.SingleOrDefaultAsync(m => m.AutorId == id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("AutorId, Name, Nacionalidade")] Autor autor)
        {
            if (id != autor.AutorId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(autor.AutorId))
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
            return View(autor);
        }
        public bool DepartamentoExists(long? id)
        {
            return _context.Autores.Any(e => e.AutorId == id);
        }
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var autor = await _context.Autores.SingleOrDefaultAsync(m => m.AutorId == id);

            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var autor = await _context.Autores.SingleOrDefaultAsync(m => m.AutorId == id);

            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var autor = await _context.Autores.SingleOrDefaultAsync(mbox => mbox.AutorId == id); ;
            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
