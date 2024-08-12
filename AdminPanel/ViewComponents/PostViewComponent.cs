using AdminPanel.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.ViewComponents;

public class  PostViewComponent : ViewComponent
{
    private readonly AdminPanelContext _context;

    public PostViewComponent(AdminPanelContext context) => _context = context;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var posts = await _context.Posts.ToListAsync();
        return View(posts);
    }

}