using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Data;
using AdminPanel.Models;

namespace AdminPanel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PopularTagsController : Controller
    {
        private readonly AdminPanelContext _context;

        public PopularTagsController(AdminPanelContext context)
        {
            _context = context;
        }

        // GET: Admin/PopularTags
        public async Task<IActionResult> Index()
        {
            return View(await _context.PopularTags.ToListAsync());
        }

        // GET: Admin/PopularTags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var popularTag = await _context.PopularTags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (popularTag == null)
            {
                return NotFound();
            }

            return View(popularTag);
        }

        // GET: Admin/PopularTags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PopularTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TagName,Id")] PopularTag popularTag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(popularTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(popularTag);
        }

        // GET: Admin/PopularTags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var popularTag = await _context.PopularTags.FindAsync(id);
            if (popularTag == null)
            {
                return NotFound();
            }
            return View(popularTag);
        }

        // POST: Admin/PopularTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TagName,Id")] PopularTag popularTag)
        {
            if (id != popularTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(popularTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PopularTagExists(popularTag.Id))
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
            return View(popularTag);
        }

        // GET: Admin/PopularTags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var popularTag = await _context.PopularTags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (popularTag == null)
            {
                return NotFound();
            }

            return View(popularTag);
        }

        // POST: Admin/PopularTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var popularTag = await _context.PopularTags.FindAsync(id);
            if (popularTag != null)
            {
                _context.PopularTags.Remove(popularTag);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PopularTagExists(int id)
        {
            return _context.PopularTags.Any(e => e.Id == id);
        }
    }
}
