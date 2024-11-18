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
    public class PedidosController : Controller
    {
        private readonly AviarasBookshopContext _context;

        public PedidosController(AviarasBookshopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pedidos = _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Livros);
            return View(await pedidos.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Livros) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
            ViewData["Livros"] = new MultiSelectList(_context.Livros, "Id", "Titulo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status,PrecoTotal,LivrosLista,ClienteId")] Pedido pedido, int[] Livros)
        {
            if (ModelState.IsValid)
            {
                pedido.Livros = await _context.Livros.Where(l => Livros.Contains(l.Id)).ToListAsync();

                foreach (int number in Livros)
                {
                    Console.WriteLine(number);
                }


                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", pedido.ClienteId);
            ViewData["Livros"] = new MultiSelectList(_context.Livros, "Id", "Titulo", Livros);
            return View(pedido);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Livros)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", pedido.ClienteId);
            ViewData["Livros"] = new MultiSelectList(_context.Livros, "Id", "Titulo", pedido.Livros.Select(l => l.Id));
            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,PrecoTotal,LivrosLista,ClienteId")] Pedido pedido, int[] livroIds)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingPedido = await _context.Pedidos
                        .Include(p => p.Livros)
                        .FirstOrDefaultAsync(p => p.Id == id);

                    if (existingPedido == null)
                    {
                        return NotFound();
                    }

                    existingPedido.Livros.Clear();
                    existingPedido.Livros = await _context.Livros.Where(l => livroIds.Contains(l.Id)).ToListAsync();

                    existingPedido.Status = pedido.Status;
                    existingPedido.PrecoTotal = pedido.PrecoTotal;
                    existingPedido.ClienteId = pedido.ClienteId;

                    _context.Update(existingPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
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

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", pedido.ClienteId);
            ViewData["Livros"] = new MultiSelectList(_context.Livros, "Id", "Titulo", livroIds);
            return View(pedido);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Livros) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Livros)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
