using AviarasBookshop.Models;
using AviarasBookshop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AviarasBookshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AviarasBookshopContext _context;

        public BooksController(AviarasBookshopContext context)
        {
            _context = context;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetBooks()
        {
            return await _context.Livros.ToListAsync();
        }
    }
}
