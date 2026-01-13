using System.Collections.Generic;
using WeatherDashboard.Models;

namespace WeatherDashboard.Services
{
    public interface IWeatherSimulationService
    {
        List<WeatherData> GenerateWeatherData(SimulationParameters parameters);
    }
}
