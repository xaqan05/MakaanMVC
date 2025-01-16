using Makaan.BL.ViewModels.Designation;
using Makaan.CORE.Enums;
using Makaan.CORE.Models;
using Makaan.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Makaan.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class DesignationController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.Designations.ToListAsync());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDesignationVM vm)
        {
            if (!ModelState.IsValid) return View();

            Designation designation = new Designation
            {
                Name = vm.Name
            };

            await _context.Designations.AddAsync(designation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Designations.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (data is null) return NotFound();

            UpdateDesignationVM vm = new UpdateDesignationVM
            {
                Name= data.Name
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id,UpdateDesignationVM vm)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Designations.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (data is null) return NotFound();

            data.Name = vm.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Designations.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (data is null) return NotFound();

            _context.Designations.Remove(data);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Hide(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Designations.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (data is null) return NotFound();

            data.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Designations.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (data is null) return NotFound();

            data.IsDeleted = false;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
