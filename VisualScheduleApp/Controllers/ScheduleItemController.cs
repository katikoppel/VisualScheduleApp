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
        public IActionResult Create(Guid scheduleId)
        {
            var result = new ScheduleItemViewModel
            {
                ScheduleId = scheduleId,
                Activities = _context.Activities.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList()
            };

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ScheduleItemViewModel vm)
        {
            if (_context.ScheduleItems.Any(x => x.ScheduleId == vm.ScheduleId && x.OrderIndex == vm.OrderIndex))
            {
                ModelState.AddModelError(nameof(vm.OrderIndex), "See järjekorranumber on juba kasutusel!");
            }

            if (_context.ScheduleItems.Any(x => x.ScheduleId == vm.ScheduleId && x.Time == vm.Time))
            {
                ModelState.AddModelError(nameof(vm.Time), "See kellaaeg on juba kasutusel!");
            }

            if (!ModelState.IsValid)
            {
                vm.Activities = _context.Activities.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

                return View("CreateUpdate", vm);
            }

            var dto = new ScheduleItemDto
            {
                Id = vm.Id,
                OrderIndex = vm.OrderIndex,
                Time = vm.Time,
                IsCompleted = false,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                ScheduleId = vm.ScheduleId,
                ActivityId = vm.ActivityId,
                ActivityName = vm.ActivityName,
                ActivityDescription = vm.ActivityDescription,
                ActivityImagePath = vm.ActivityImagePath
            };

            await _scheduleItemServices.CreateAsync(dto);

            return RedirectToAction("Details", "Schedule", new { id = vm.ScheduleId });
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
                Time = scheduleItem.Time,
                IsCompleted = scheduleItem.IsCompleted,
                CreatedAt = scheduleItem.CreatedAt,
                ModifiedAt = scheduleItem.ModifiedAt,
                ScheduleId = scheduleItem.ScheduleId,
                ActivityId = scheduleItem.ActivityId,
                ActivityName = scheduleItem.ActivityName,
                ActivityDescription = scheduleItem.ActivityDescription,
                ActivityImagePath = scheduleItem.ActivityImagePath,
                Activities = _context.Activities.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList()
            };

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ScheduleItemViewModel vm)
        {
            if (_context.ScheduleItems.Any(x => x.ScheduleId == vm.ScheduleId && x.OrderIndex == vm.OrderIndex && x.Id != vm.Id))
            {
                ModelState.AddModelError(nameof(vm.OrderIndex), "See järjekorranumber on juba kasutusel!");
            }

            if (_context.ScheduleItems.Any(x => x.ScheduleId == vm.ScheduleId && x.Time == vm.Time && x.Id != vm.Id))
            {
                ModelState.AddModelError(nameof(vm.Time), "See kellaaeg on juba kasutusel!");
            }

            if (!ModelState.IsValid)
            {
                vm.Activities = _context.Activities.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

                return View("CreateUpdate", vm);
            }

            var dto = new ScheduleItemDto
            {
                Id = vm.Id,
                OrderIndex = vm.OrderIndex,
                Time = vm.Time,
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

            return RedirectToAction("Details", "Schedule", new { id = vm.ScheduleId });
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
                Time = scheduleItem.Time,
                IsCompleted = scheduleItem.IsCompleted,
                CreatedAt = scheduleItem.CreatedAt,
                ModifiedAt = scheduleItem.ModifiedAt,
                ScheduleId = scheduleItem.ScheduleId,
                ActivityId = scheduleItem.ActivityId,
                ActivityName = scheduleItem.ActivityName,
                ActivityDescription = scheduleItem.ActivityDescription,
                ActivityImagePath = scheduleItem.ActivityImagePath
            };

            ViewBag.ScheduleId = scheduleItem.ScheduleId;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, Guid scheduleId)
        {
            await _scheduleItemServices.DeleteAsync(id);
            return RedirectToAction("Details", "Schedule", new { id = scheduleId });
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
                Time = scheduleItem.Time,
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
        public async Task<IActionResult> CompletionToggle(Guid id)
        {
            var item = await _scheduleItemServices.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            item.IsCompleted = !item.IsCompleted;

            await _scheduleItemServices.UpdateAsync(item);

            return RedirectToAction("Details", "Schedule", new { id = item.ScheduleId });
        }
    }
}
