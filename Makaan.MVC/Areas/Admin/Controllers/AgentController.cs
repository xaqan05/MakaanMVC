using Makaan.BL.ViewModels.Agent;
using Makaan.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Makaan.BL.Extensions;
using Makaan.CORE.Models;
using Makaan.CORE.Enums;
using Microsoft.AspNetCore.Authorization;
namespace Makaan.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class AgentController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agents.Include(x => x.Designation).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Designation = await _context.Designations.Where(x => !x.IsDeleted).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAgentVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Designation = await _context.Designations.Where(x => !x.IsDeleted).ToListAsync();
                return View();
            };

            if (!vm.Image.IsValidType("image"))
            {
                ModelState.AddModelError("Image", "File must be image");
                return View();
            }
            if (!vm.Image.IsValidSize(5 * 1024))
            {
                ModelState.AddModelError("Image", "File size must be less than 5MB");
                return View();
            }

            string newFileName = await vm.Image.UploadAsync(_env.WebRootPath, "imgs", "agent");

            Agent agent = new Agent
            {
                FullName = vm.FullName,
                DesignationId = vm.DesignationId,
                ImageUrl = newFileName
            };

            await _context.Agents.AddAsync(agent);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Designation = await _context.Designations.Where(x => !x.IsDeleted).ToListAsync();

            if (!id.HasValue) return BadRequest();

            var data = await _context.Agents.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (data is null) return NotFound();

            UpdateAgentVM vm = new UpdateAgentVM
            {
                FullName = data.FullName,
                DesignationId = data.DesignationId
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAgentVM vm)
        {

            if (!id.HasValue) return BadRequest();

            var data = await _context.Agents.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (data is null) return NotFound();

            ViewBag.Designation = await _context.Designations.Where(x => !x.IsDeleted).ToListAsync();

            if (!ModelState.IsValid)
            {
                ViewBag.Designation = await _context.Designations.Where(x => !x.IsDeleted).ToListAsync();
                return View();
            };

            if (vm.Image != null)
            {
                if (!vm.Image.IsValidType("image"))
                {
                    ModelState.AddModelError("Image", "File must be image");
                    return View();
                }
                if (!vm.Image.IsValidSize(5 * 1024))
                {
                    ModelState.AddModelError("Image", "File size must be less than 5MB");
                    return View();
                }

                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), _env.WebRootPath, "imgs", "agent", data.ImageUrl);

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                string newFileName = await vm.Image.UploadAsync(_env.WebRootPath, "imgs", "agent");
                data.ImageUrl = newFileName;
            }

            data.FullName = vm.FullName;
            data.DesignationId = vm.DesignationId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Agents.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (data is null) return NotFound();

            _context.Agents.Remove(data);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Hide(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Agents.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (data is null) return NotFound();

            data.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Agents.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (data is null) return NotFound();

            data.IsDeleted = false;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
