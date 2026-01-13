using WeatherDashboard.Models;

namespace WeatherDashboard.Services
{
    public class WeatherSimulationService : IWeatherSimulationService
    {
        private readonly Random _random = new Random();

        public List<WeatherData> GenerateWeatherData(SimulationParameters parameters)
        {
            var data = new List<WeatherData>();
            var startDate = DateTime.Today;

            // East European climate characteristics:
            // - Large temperature swings between summer and winter.
            // - Continental influence.
            // - Average temp varies significantly by month.

            for (int i = 0; i < parameters.DaysToDisplay; i++)
            {
                var currentDate = startDate.AddDays(i);
                int dayOfYear = currentDate.DayOfYear;

                // Base temperature based on time of year (seasonal cycle)
                // - January (day 15) is coldest: ~ -3°C
                // - July (day 196) is hottest: ~ 20°C
                // We use a cosine wave shifted to have minimum in mid-January.
                double seasonalAvg = 8.5 + 11.5 * Math.Cos(2 * Math.PI * (dayOfYear - 196) / 365.25);
                
                // Daily fluctuation (simulating random weather changes)
                double randomFactor = (_random.NextDouble() - 0.5) * 2.0;

                double temp = seasonalAvg + randomFactor;
                
                // Adjust by parameters.AverageTemperature as an offset if provided
                // Default AverageTemperature is 20, let's treat it as a target avg if it deviates from 20
                temp += (parameters.AverageTemperature - 20.0);

                // Rainfall: East Europe has moderate rainfall, often convective in summer (thunderstorms)
                // or steady in autumn/spring.
                double rainfall = 0;
                if (_random.NextDouble() < 0.3) // 30% chance of rain
                {
                    rainfall = _random.NextDouble() * 15.0;
                    if (_random.NextDouble() > 0.8) rainfall *= 3.0; // Heavy burst
                }

                // Wind Speed: Higher in spring/autumn transition
                double windBase = 10.0;
                if (currentDate.Month == 3 || currentDate.Month == 10) windBase = 18.0;
                double windSpeed = windBase + _random.NextDouble() * 20.0;
                if (_random.NextDouble() > 0.95) windSpeed += 30.0; // Gusts

                var weatherEntry = new WeatherData
                {
                    Date = currentDate,
                    Temperature = Math.Round(temp, 1),
                    Rainfall = Math.Round(rainfall, 1),
                    WindSpeed = Math.Round(windSpeed, 1)
                };

                weatherEntry.HeatIndex = CalculateHeatIndex(weatherEntry.Temperature);
                weatherEntry.StormProbability = CalculateStormProbability(weatherEntry.Rainfall, weatherEntry.WindSpeed);

                data.Add(weatherEntry);
            }

            if (data.Any())
            {
                var metrics = new WeatherMetrics
                {
                    AverageTemperature = Math.Round(data.Average(d => d.Temperature), 1),
                    TotalRainfall = Math.Round(data.Sum(d => d.Rainfall), 1),
                    MaxWindSpeed = Math.Round(data.Max(d => d.WindSpeed), 1)
                };

                foreach (var entry in data)
                {
                    entry.Summary = metrics;
                }
            }

            return data;
        }

        private double CalculateHeatIndex(double temp)
        {
            // Simplified heat index calculation
            // In a real app, this would use humidity too.
            if (temp < 27) return temp;
            return Math.Round(temp + (temp * 0.05), 1);
        }

        private double CalculateStormProbability(double rainfall, double windSpeed)
        {
            // Simple heuristic for storm probability
            double prob = (rainfall * 2.0) + (windSpeed * 1.5);
            return Math.Clamp(Math.Round(prob, 1), 0, 100);
        }
    }
}
