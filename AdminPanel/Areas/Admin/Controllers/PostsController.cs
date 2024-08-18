using AdminPanel.Data;
using AdminPanel.Models;
using ElmahCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly AdminPanelContext _context;

        public PostsController(AdminPanelContext context)
        {
            _context = context;
        }

        // GET: Admin/Posts
        public async Task<IActionResult> Index()
        {
            // HttpContext.RaiseError(new Exception("test"));

            var adminPanelContext = await _context
                 .Posts
                .Include(p => p.Category)
                .Include(p => p.PostsPopularTag)
                .ThenInclude(p => p.PopularTag)
                .AsNoTracking()
                .ToListAsync();
            return View(adminPanelContext);
        }

        // GET: Admin/Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Admin/Posts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["PostsPopularTag"] = new SelectList(_context.PopularTags, "Id", "TagName");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post, int[] PostPopularTags, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (PostPopularTags.Length > 0)
                {
                    post.PostsPopularTag = PostPopularTags
                        .Select(t => new PopularTagPost { PopularTagId = t })
                        .ToList();
                }

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
            ViewData["PostsPopularTag"] = new SelectList(_context.PopularTags, "Id", "TagName");

            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.
                Include(p => p.Category)
                .Include(p => p.PostsPopularTag)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
            ViewData["PostsPopularTag"] = new SelectList(_context.PopularTags, "Id", "TagName");
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post, int[] PostsPopularTag)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var exisingTags = _context.PopularTagPost.Where(pt => pt.PostId == post.Id);

                    _context.PopularTagPost.RemoveRange(exisingTags);

                    if (PostsPopularTag != null && PostsPopularTag.Any())
                    {
                        post.PostsPopularTag = PostsPopularTag
                        .Select(t => new PopularTagPost { PopularTagId = t })
                        .ToList();
                    }

                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    HttpContext.RaiseError(ex);
                    if (!PostExists(post.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
            ViewData["PostsPopularTag"] = new SelectList(_context.PopularTags, "Id", "TagName");
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }


        public IActionResult Upload(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }


            return View(model: id);
        }



        [HttpPost]
        public async Task<IActionResult> Upload(int? id, IFormFile image, CancellationToken cancellationToken)
        {

            if (!id.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }

            if (image == null)
            {
                return BadRequest("Image is null");
            }

            if (image.Length <= 0)
            {
                return BadRequest("Image is empty");
            }


            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant(); // ToLowerInvariant => Küçük harfe çevirir. (pc dilinden bağımsız)

            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
            {
                return BadRequest("Invalid image extension");
            }

            //Guid.NewGuid() -> D516A121-4F41-4715-95BD-99E91B92DE84  
            string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload", fileName);

            try
            {
                await using (var stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
                //throw;
            }

            var imageUlr = $"{Request.Scheme}://{Request.Host}/upload/{fileName}";
            // post request image stock api

            var post = await _context.Posts.FindAsync(id);
            post.ImageUrl = imageUlr;

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
