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
    public class CategoriaController : Controller
    {
        public readonly LivrosContext _context;
        public CategoriaController(LivrosContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.OrderBy(c => c.Name).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Description")] Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(categoria);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados.");
            }
            return View(categoria);
        }
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoria = await _context.Categorias.SingleOrDefaultAsync(m => m.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("CategoriaId, Name, Description")] Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(categoria.CategoriaId))
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
            return View(categoria);
        }
        public bool DepartamentoExists(long? id)
        {
            return _context.Categorias.Any(e => e.CategoriaId == id);
        }
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoria = await _context.Categorias.SingleOrDefaultAsync(m => m.CategoriaId == id);

            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoria = await _context.Categorias.SingleOrDefaultAsync(m => m.CategoriaId == id);

            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var categoria = await _context.Categorias.SingleOrDefaultAsync(mbox => mbox.CategoriaId == id); ;
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }

}

