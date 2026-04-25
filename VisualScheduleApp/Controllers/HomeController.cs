using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VisualScheduleApp.Core.ServiceInterface;
using VisualScheduleApp.Models;

namespace VisualScheduleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScheduleServices _scheduleServices;

        public HomeController(
            ILogger<HomeController> logger, 
            IScheduleServices scheduleServices
            )
        {
            _logger = logger;
            _scheduleServices = scheduleServices;
        }

        public async Task<IActionResult> Index()
        {
            var schedule = await _scheduleServices.GetTodayAsync();
            return View(schedule);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}