using AdminPanel.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.ViewComponents;

public class CategoryViewComponent : ViewComponent
{

    //private readonly ICategoryService _context;

    private readonly AdminPanelContext _context;
    public CategoryViewComponent(AdminPanelContext context) => _context = context;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return View(categories);
    }

}
