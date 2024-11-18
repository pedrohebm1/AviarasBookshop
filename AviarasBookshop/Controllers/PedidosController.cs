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

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var pedidos = _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Livros); // Inclui os livros no pedido
            return View(await pedidos.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Livros) // Inclui os livros no pedido
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
            ViewData["Livros"] = new MultiSelectList(_context.Livros, "Id", "Titulo"); // Preenche os livros disponíveis
            return View();
        }

        // POST: Pedidos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status,PrecoTotal,LivrosLista,ClienteId")] Pedido pedido, int[] Livros)
        {
            if (ModelState.IsValid)
            {
                // Associa os livros selecionados ao pedido
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

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Livros) // Carrega os livros associados
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", pedido.ClienteId);
            ViewData["Livros"] = new MultiSelectList(_context.Livros, "Id", "Titulo", pedido.Livros.Select(l => l.Id));
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
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
                    // Atualiza os dados do pedido
                    var existingPedido = await _context.Pedidos
                        .Include(p => p.Livros)
                        .FirstOrDefaultAsync(p => p.Id == id);

                    if (existingPedido == null)
                    {
                        return NotFound();
                    }

                    // Atualiza os livros associados
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

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Livros) // Inclui os livros no pedido
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Livros)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido != null)
            {
                // Remove o pedido
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
