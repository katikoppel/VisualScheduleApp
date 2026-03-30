using Microsoft.AspNetCore.Mvc;
using VisualScheduleApp.Core.Dto;
using VisualScheduleApp.Core.ServiceInterface;
using VisualScheduleApp.Data;
using VisualScheduleApp.Models.Children;

namespace VisualScheduleApp.Controllers
{
    public class ChildController : Controller
    {
        private readonly VisualScheduleAppContext _context;
        private readonly IChildServices _childServices;

        public ChildController
        (
            VisualScheduleAppContext context,
            IChildServices childServices
        )
        {
            _context = context;
            _childServices = childServices;
        }

        public IActionResult Index()
        {
            var result = _context.Children
                .Select(x => new ChildViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    BirthDate = x.BirthDate
                })
                .ToList();

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ChildViewModel result = new();

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChildViewModel vm)
        {
            var dto = new ChildDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                BirthDate = vm.BirthDate,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _childServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var child = await _childServices.DetailAsync(id);

            if (child == null)
            {
                return NotFound();
            }

            var vm = new ChildViewModel();

            vm.Id = child.Id;
            vm.Name = child.Name;
            vm.BirthDate = child.BirthDate;
            vm.CreatedAt = child.CreatedAt;
            vm.ModifiedAt = child.ModifiedAt;

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ChildViewModel vm)
        {
            var dto = new ChildDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                BirthDate = vm.BirthDate,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _childServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var child = await _childServices.DetailAsync(id);

            if (child == null)
            {
                return NotFound();
            }

            var vm = new ChildViewModel();

            vm.Id = child.Id;
            vm.Name = child.Name;
            vm.BirthDate = child.BirthDate;
            vm.CreatedAt = child.CreatedAt;
            vm.ModifiedAt = child.ModifiedAt;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var child = await _childServices.Delete(id);

            if (child == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var child = await _childServices.DetailAsync(id);

            if (child == null)
            {
                return NotFound();
            }

            var vm = new ChildViewModel();

            vm.Id = child.Id;
            vm.Name = child.Name;
            vm.BirthDate = child.BirthDate;
            vm.CreatedAt = child.CreatedAt;
            vm.ModifiedAt = child.ModifiedAt;

            return View(vm);
        }
    }
}