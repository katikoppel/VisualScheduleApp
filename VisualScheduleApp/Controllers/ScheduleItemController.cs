using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisualScheduleApp.Core.Dto;
using VisualScheduleApp.Core.ServiceInterface;
using VisualScheduleApp.Data;
using VisualScheduleApp.Models.ScheduleItems;


namespace VisualScheduleApp.Controllers
{
    public class ScheduleItemController : Controller
    {
        private readonly VisualScheduleAppContext _context;
        private readonly IScheduleItemServices _scheduleItemServices;

        public ScheduleItemController(
            VisualScheduleAppContext context,
            IScheduleItemServices scheduleItemServices)
        {
            _context = context;
            _scheduleItemServices = scheduleItemServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid scheduleId)
        {
            var scheduleItems = await _scheduleItemServices.GetAllByScheduleIdAsync(scheduleId);

            var result = scheduleItems.Select(x => new ScheduleItemViewModel
            {
                Id = x.Id,
                OrderIndex = x.OrderIndex,
                IsCompleted = x.IsCompleted,
                CreatedAt = x.CreatedAt,
                ModifiedAt = x.ModifiedAt,
                ScheduleId = x.ScheduleId,
                ActivityId = x.ActivityId,
                ActivityName = x.ActivityName,
                ActivityDescription = x.ActivityDescription,
                ActivityImagePath = x.ActivityImagePath
            }).ToList();

            ViewBag.ScheduleId = scheduleId;
            return View(result);
        }

        [HttpGet]
        public IActionResult Create(Guid scheduleId)
        {
            var result = new ScheduleItemViewModel
            {
                ScheduleId = scheduleId
            };

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ScheduleItemViewModel vm)
        {
            var dto = new ScheduleItemDto
            {
                Id = vm.Id,
                OrderIndex = vm.OrderIndex,
                IsCompleted = vm.IsCompleted,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                ScheduleId = vm.ScheduleId,
                ActivityId = vm.ActivityId,
                ActivityName = vm.ActivityName,
                ActivityDescription = vm.ActivityDescription,
                ActivityImagePath = vm.ActivityImagePath
            };

            await _scheduleItemServices.CreateAsync(dto);

            return RedirectToAction(nameof(Index), new { scheduleId = vm.ScheduleId });
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var scheduleItem = await _scheduleItemServices.GetByIdAsync(id);

            if (scheduleItem == null)
            {
                return NotFound();
            }

            var vm = new ScheduleItemViewModel
            {
                Id = scheduleItem.Id,
                OrderIndex = scheduleItem.OrderIndex,
                IsCompleted = scheduleItem.IsCompleted,
                CreatedAt = scheduleItem.CreatedAt,
                ModifiedAt = scheduleItem.ModifiedAt,
                ScheduleId = scheduleItem.ScheduleId,
                ActivityId = scheduleItem.ActivityId,
                ActivityName = scheduleItem.ActivityName,
                ActivityDescription = scheduleItem.ActivityDescription,
                ActivityImagePath = scheduleItem.ActivityImagePath
            };

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ScheduleItemViewModel vm)
        {
            var dto = new ScheduleItemDto
            {
                Id = vm.Id,
                OrderIndex = vm.OrderIndex,
                IsCompleted = vm.IsCompleted,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                ScheduleId = vm.ScheduleId,
                ActivityId = vm.ActivityId,
                ActivityName = vm.ActivityName,
                ActivityDescription = vm.ActivityDescription,
                ActivityImagePath = vm.ActivityImagePath
            };

            await _scheduleItemServices.UpdateAsync(dto);

            return RedirectToAction(nameof(Index), new { scheduleId = vm.ScheduleId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var scheduleItem = await _scheduleItemServices.GetByIdAsync(id);

            if (scheduleItem == null)
            {
                return NotFound();
            }

            var vm = new ScheduleItemViewModel
            {
                Id = scheduleItem.Id,
                OrderIndex = scheduleItem.OrderIndex,
                IsCompleted = scheduleItem.IsCompleted,
                CreatedAt = scheduleItem.CreatedAt,
                ModifiedAt = scheduleItem.ModifiedAt,
                ScheduleId = scheduleItem.ScheduleId,
                ActivityId = scheduleItem.ActivityId,
                ActivityName = scheduleItem.ActivityName,
                ActivityDescription = scheduleItem.ActivityDescription,
                ActivityImagePath = scheduleItem.ActivityImagePath
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id, Guid scheduleId)
        {
            await _scheduleItemServices.DeleteAsync(id);
            return RedirectToAction(nameof(Index), new { scheduleId });
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var scheduleItem = await _scheduleItemServices.GetByIdAsync(id);

            if (scheduleItem == null)
            {
                return NotFound();
            }

            var vm = new ScheduleItemViewModel
            {
                Id = scheduleItem.Id,
                OrderIndex = scheduleItem.OrderIndex,
                IsCompleted = scheduleItem.IsCompleted,
                CreatedAt = scheduleItem.CreatedAt,
                ModifiedAt = scheduleItem.ModifiedAt,
                ScheduleId = scheduleItem.ScheduleId,
                ActivityId = scheduleItem.ActivityId,
                ActivityName = scheduleItem.ActivityName,
                ActivityDescription = scheduleItem.ActivityDescription,
                ActivityImagePath = scheduleItem.ActivityImagePath
            };

            return View(vm);
        }
    }
}
