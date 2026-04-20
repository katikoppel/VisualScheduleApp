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
            ScheduleViewModel result = new();
            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ScheduleViewModel vm)
        {
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
                ModifiedAt = schedule.ModifiedAt
            };

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ScheduleViewModel vm)
        {
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
    }
}
