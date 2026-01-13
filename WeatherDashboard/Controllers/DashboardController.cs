using Microsoft.AspNetCore.Mvc;
using WeatherDashboard.Models;
using WeatherDashboard.Services;

namespace WeatherDashboard.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IWeatherSimulationService _simulationService;

        public DashboardController(IWeatherSimulationService simulationService)
        {
            _simulationService = simulationService;
        }

        public IActionResult Index()
        {
            var parameters = new SimulationParameters();
            var data = _simulationService.GenerateWeatherData(parameters);
            
            ViewBag.Parameters = parameters;
            return View(data);
        }

        [HttpPost]
        public IActionResult Simulate(SimulationParameters parameters)
        {
            var data = _simulationService.GenerateWeatherData(parameters);
            return Json(data);
        }
    }
}
