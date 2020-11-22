using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livros.Models;
using Livros.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Livros.Controllers
{
    public class LivroController : Controller
    {
        private readonly LivrosContext _context;
        public LivroController(LivrosContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Livros.Include(i=> i.Autor).Include(g=> g.Categoria).OrderBy(c=> c.Titulo).ToListAsync());
        }
        public IActionResult Create()
        {
            var autores = _context.Autores.OrderBy(i => i.Name).ToList();
            autores.Insert(0, new Autor() { AutorId = 0, Name = "Selecione o Autor" });
            ViewBag.Autores = autores;

            var categorias = _context.Categorias.OrderBy(g => g.Name).ToList();
            categorias.Insert(0, new Categoria() { CategoriaId = 0, Name = "Selecione uma categoria" });
            ViewBag.Categorias = categorias;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo, Ano, ISBN, AutorId, CategoriaId")] Livro livro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(livro);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados.");
            }
            return View(livro);
        }
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var livro = await _context.Livros.SingleOrDefaultAsync(m => m.LivroId == id);
            if (livro == null)
            {
                return NotFound();
            }
            ViewBag.Autores = new SelectList(_context.Autores.OrderBy(i => i.Name), "AutorId", "Name", livro.AutorId);
            ViewBag.Categorias = new SelectList(_context.Categorias.OrderBy(g => g.Name), "CategoriaId", "Name", livro.CategoriaId);
            return View(livro);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("LivroId, Titulo, Ano, ISBN, AutorId, CategoriaId")] Livro livro)
        {
            if (id != livro.LivroId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.LivroId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.Autores = new SelectList(_context.Autores.OrderBy(i => i.Name), "AutorId", "Name", livro.AutorId);
                ViewBag.Categorias = new SelectList(_context.Categorias.OrderBy(g => g.Name), "CategoriaId", "Name", livro.CategoriaId);
                return RedirectToAction(nameof(Index));
            }
            
            return View(livro);
        }
        public bool LivroExists(long? id)
        {
            return _context.Autores.Any(e => e.AutorId == id);
        }
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var livro = await _context.Livros.SingleOrDefaultAsync(m => m.LivroId == id);
            _context.Autores.Where(i => livro.AutorId == i.AutorId).Load();
            _context.Categorias.Where(g => livro.CategoriaId == g.CategoriaId).Load();

            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var livro = await _context.Livros.SingleOrDefaultAsync(m => m.LivroId == id);
            _context.Autores.Where(i => i.AutorId == livro.AutorId).Load();
            _context.Categorias.Where(g => g.CategoriaId == livro.CategoriaId).Load();
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var livro = await _context.Livros.SingleOrDefaultAsync(mbox => mbox.LivroId == id); ;
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
