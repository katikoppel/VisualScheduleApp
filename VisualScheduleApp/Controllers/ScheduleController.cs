using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisualScheduleApp.Core.Dto;
using VisualScheduleApp.Data;
using VisualScheduleApp.Core.ServiceInterface;
using VisualScheduleApp.Models.Schedules;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace VisualScheduleApp.Controllers
{
    [Authorize]
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = _context.Schedules
                .Include(x => x.Child)
                .Where(x => x.UserId == userId)
                .Select(x => new ScheduleViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    ChildId = x.ChildId,
                    ChildName = x.Child != null ? x.Child.Name : null,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt,
                    UserId = x.UserId
                })
                .ToList();

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ScheduleViewModel result = new()
            {
                Children = _context.Children
                    .Where(x => x.UserId == userId)
                    .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!ModelState.IsValid)
            {
                vm.Children = _context.Children
                    .Where(x => x.UserId == userId)
                    .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
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
                ModifiedAt = vm.ModifiedAt,
                UserId = userId
            };

            await _scheduleServices.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var schedule = await _scheduleServices.GetByIdAsync(id);

            if (schedule == null || schedule.UserId != userId)
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
                UserId = schedule.UserId,
                Children = _context.Children
                    .Where(x => x.UserId == userId)
                    .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var existingSchedule = await _scheduleServices.GetByIdAsync(vm.Id);

            if (existingSchedule == null || existingSchedule.UserId != userId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                vm.Children = _context.Children
                    .Where(x => x.UserId == userId)
                    .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
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
                ModifiedAt = vm.ModifiedAt,
                UserId = userId
            };

            await _scheduleServices.UpdateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var schedule = await _scheduleServices.GetByIdAsync(id);

            if (schedule == null || schedule.UserId != userId)
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
                UserId = schedule.UserId
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var schedule = await _scheduleServices.GetByIdAsync(id);

            if (schedule == null || schedule.UserId != userId)
            {
                return NotFound();
            }

            await _scheduleServices.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var schedule = await _context.Schedules
                .Include(x => x.Child)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

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
                UserId = schedule.UserId,
                ScheduleItems = scheduleItems
            };

            return View(vm);
        }
    }
}
