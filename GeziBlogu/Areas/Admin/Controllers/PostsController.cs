using GeziBlogu.Data;
using GeziBlogu.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GeziBlogu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly DatabaseContext _context;

        public PostsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: PostsController
        public async Task<ActionResult> Index()
        {
            var posts =await _context.Posts.ToListAsync();
            return View(posts);
        }

        // GET: PostsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostsController/Create
        public async Task<ActionResult> CreateAsync()
        {
            var category = await _context.Categories.ToListAsync();
            ViewBag.CategoryId = new SelectList(category, "Id", "Name");
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Post post, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null)
                    {
                        string directory = Directory.GetCurrentDirectory() + "/wwwroot/img/" + Image.FileName;
                        using var stream=new FileStream(directory, FileMode.Create);
                        await Image.CopyToAsync(stream);
                        post.Image = Image.FileName;
                    }
                    await _context.Posts.AddAsync(post);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            
            var category = await _context.Categories.ToListAsync();
            ViewBag.CategoryId = new SelectList(category, "Id", "Name");
            return View();
        }

        // GET: PostsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            var category = await _context.Categories.ToListAsync();
            ViewBag.CategoryId = new SelectList(category, "Id", "Name");
            return View(post);
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Post post,IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null)
                    {
                        string directory = Directory.GetCurrentDirectory() + "/wwwroot/img/" + Image.FileName;
                        using var stream = new FileStream(directory, FileMode.Create);
                        await Image.CopyToAsync(stream);
                        post.Image = Image.FileName;
                    }
                    _context.Posts.Update(post);
                   await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            var category = await _context.Categories.ToListAsync();
            ViewBag.CategoryId = new SelectList(category, "Id", "Name");
            return View(post);
        }

        // GET: PostsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var post =await _context.Posts.FindAsync(id);
            return View(post);
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Post post)
        {
            try
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
