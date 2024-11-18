using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AviarasBookshop.Data;
using AviarasBookshop.Models;

namespace AviarasBookshop.Controllers
{
    public class LivrosController : Controller
    {
        private readonly AviarasBookshopContext _context;

        public LivrosController(AviarasBookshopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var livros = _context.Livros.Include(l => l.Autores);
            return View(await livros.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .Include(l => l.Autores)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        public IActionResult Create()
        {
            ViewData["Autores"] = new MultiSelectList(_context.Autores, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Categoria,Preco")] Livro livro, int[] Autores)
        {
            if (ModelState.IsValid)
            {
                if (Autores != null && Autores.Length > 0)
                {
                    livro.Autores = await _context.Autores.Where(a => Autores.Contains(a.Id)).ToListAsync();
                }

                _context.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Autores"] = new MultiSelectList(_context.Autores, "Id", "Nome", Autores);
            return View(livro);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            ViewData["Autores"] = new MultiSelectList(_context.Autores, "Id", "Nome", livro.Autores.Select(a => a.Id));
            return View(livro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Categoria,Preco")] Livro livro, int[] Autores)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    livro.Autores = await _context.Autores.Where(a => Autores.Contains(a.Id)).ToListAsync();

                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
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

            ViewData["Autores"] = new MultiSelectList(_context.Autores, "Id", "Nome", livro.Autores.Select(a => a.Id));
            return View(livro);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .Include(l => l.Autores)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro != null)
            {
                _context.Livros.Remove(livro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int id)
        {
            return _context.Livros.Any(e => e.Id == id);
        }
    }
}
