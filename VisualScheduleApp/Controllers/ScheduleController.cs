using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisualScheduleApp.Core.Dto;
using VisualScheduleApp.Data;
using VisualScheduleApp.Core.ServiceInterface;
using VisualScheduleApp.Models.Schedules;

namespace VisualScheduleApp.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly VisualScheduleAppContext _context;
        private readonly IScheduleServices _scheduleServices;

        public ScheduleController(
            VisualScheduleAppContext context,
            IScheduleServices scheduleServices)
        {
            _context = context;
            _scheduleServices = scheduleServices;
        }

        public async Task<IActionResult> Index()
        {
            var result = _context.Schedules
                .Include(x => x.Child)
                .Select(x => new ScheduleViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    ChildId = x.ChildId,
                    ChildName = x.Child != null ? x.Child.Name : null,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt
                })
                .ToList();

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ScheduleViewModel result = new()
            {
                Children = _context.Children.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList()
            };

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ScheduleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Children = _context.Children.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

                return View("CreateUpdate", vm);
            }

            var dto = new ScheduleDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Date = vm.Date,
                ChildId = vm.ChildId,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            await _scheduleServices.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var schedule = await _scheduleServices.GetByIdAsync(id);

            if (schedule == null)
            {
                return NotFound();
            }

            var vm = new ScheduleViewModel
            {
                Id = schedule.Id,
                Name = schedule.Name,
                Date = schedule.Date,
                ChildId = schedule.ChildId,
                CreatedAt = schedule.CreatedAt,
                ModifiedAt = schedule.ModifiedAt,
                Children = _context.Children.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList()
            };

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ScheduleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Children = _context.Children.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

                return View("CreateUpdate", vm);
            }

            var dto = new ScheduleDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Date = vm.Date,
                ChildId = vm.ChildId,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            await _scheduleServices.UpdateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var schedule = await _scheduleServices.GetByIdAsync(id);

            if (schedule == null)
            {
                return NotFound();
            }

            var vm = new ScheduleViewModel
            {
                Id = schedule.Id,
                Name = schedule.Name,
                Date = schedule.Date,
                ChildId = schedule.ChildId,
                CreatedAt = schedule.CreatedAt,
                ModifiedAt = schedule.ModifiedAt
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            await _scheduleServices.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var schedule = await _context.Schedules
                .Include(x => x.Child)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (schedule == null)
            {
                return NotFound();
            }

            var scheduleItems = await _context.ScheduleItems
                .Include(x => x.Activity)
                .Where(x => x.ScheduleId == id)
                .OrderBy(x => x.OrderIndex)
                .Select(x => new VisualScheduleApp.Models.ScheduleItems.ScheduleItemViewModel
                {
                    Id = x.Id,
                    OrderIndex = x.OrderIndex,
                    Time = x.Time,
                    IsCompleted = x.IsCompleted,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt,
                    ScheduleId = x.ScheduleId,
                    ActivityId = x.ActivityId,
                    ActivityName = x.Activity != null ? x.Activity.Name : null,
                    ActivityDescription = x.Activity != null ? x.Activity.Description : null,
                    ActivityImagePath = x.Activity != null ? x.Activity.ImagePath : null
                })
                .ToListAsync();

            var vm = new ScheduleViewModel
            {
                Id = schedule.Id,
                Name = schedule.Name,
                Date = schedule.Date,
                ChildId = schedule.ChildId,
                ChildName = schedule.Child != null ? schedule.Child.Name : null,
                CreatedAt = schedule.CreatedAt,
                ModifiedAt = schedule.ModifiedAt,
                ScheduleItems = scheduleItems
            };

            return View(vm);
        }
    }
}
