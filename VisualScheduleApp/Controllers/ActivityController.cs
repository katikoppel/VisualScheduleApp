using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisualScheduleApp.Core.Dto;
using VisualScheduleApp.Core.ServiceInterface;
using VisualScheduleApp.Data;
using VisualScheduleApp.Models.Activities;

namespace VisualScheduleApp.Controllers
{
    public class ActivityController : Controller
    {
        private readonly VisualScheduleAppContext _context;
        private readonly IActivityServices _activityServices;
        private readonly IFileServices _fileServices;

        public ActivityController(
            VisualScheduleAppContext context,
            IActivityServices activityServices,
            IFileServices fileServices)
        {
            _context = context;
            _activityServices = activityServices;
            _fileServices = fileServices;
        }

        public IActionResult Index()
        {
            var result = _context.Activities
                .Select(x => new ActivityViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt
                })
                .ToList();

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ActivityViewModel result = new();
            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityViewModel vm)
        {
            var dto = new ActivityDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files
            };

            await _activityServices.Create(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var activity = await _activityServices.DetailAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            var vm = new ActivityViewModel
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                ImagePath = activity.ImagePath,
                CreatedAt = activity.CreatedAt,
                ModifiedAt = activity.ModifiedAt
            };

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ActivityViewModel vm)
        {
            var dto = new ActivityDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files
            };

            await _activityServices.Update(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var activity = await _activityServices.DetailAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            var vm = new ActivityViewModel
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                ImagePath = activity.ImagePath,
                CreatedAt = activity.CreatedAt,
                ModifiedAt = activity.ModifiedAt
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var activity = await _activityServices.Delete(id);

            if (activity == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var activity = await _activityServices.DetailAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            var vm = new ActivityViewModel
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                ImagePath = activity.ImagePath,
                CreatedAt = activity.CreatedAt,
                ModifiedAt = activity.ModifiedAt
            };

            return View(vm);
        }
    }
}