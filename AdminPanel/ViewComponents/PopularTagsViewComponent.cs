using AdminPanel.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.ViewComponents;

public class PopularTagsViewComponent : ViewComponent
{
    private readonly AdminPanelContext _context;
    public PopularTagsViewComponent(AdminPanelContext context) => _context = context;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var tags = await _context.PopularTags.ToListAsync();
        return View(tags);
    }
}