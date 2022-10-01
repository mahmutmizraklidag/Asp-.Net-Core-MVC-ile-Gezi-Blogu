using GeziBlogu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeziBlogu.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _context;

        public CategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync(int id)
        {
            //var model = await _context.Categories.FindAsync(id);
            var model = await _context.Categories.Include(p => p.Posts).FirstOrDefaultAsync(c => c.Id == id);
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            //var model = await _context.Categories.FindAsync(id);
            var model = await _context.Posts.FindAsync(id);
            return View(model);
        }
    }
}
