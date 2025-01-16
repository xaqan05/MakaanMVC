using Makaan.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Makaan.MVC.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agents.Where(x => !x.IsDeleted).Include(x => x.Designation).ToListAsync());
        }
    }
}
