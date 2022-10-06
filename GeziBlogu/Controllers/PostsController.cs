using GeziBlogu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeziBlogu.Controllers
{
    public class PostsController : Controller
    {
        private readonly DatabaseContext _context;

        public PostsController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync(string? kelime)
        {
            if (kelime != null)
            {
                var searchResult = await _context.Posts.Where(p => p.Title.Contains(kelime) || p.Description.Contains(kelime)).ToListAsync();
                return View(searchResult);
            }
            var list = await _context.Posts.ToListAsync();
            return View(list);
        }
        public async Task<IActionResult> DetailAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
    }
}
