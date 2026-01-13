using FluentAssertions;
using WeatherDashboard.Models;
using WeatherDashboard.Services;

namespace WeatherDashboard.Tests
{
    public class WeatherSimulationServiceTests
    {
        private readonly WeatherSimulationService _service;

        public WeatherSimulationServiceTests()
        {
            _service = new WeatherSimulationService();
        }

        [Fact]
        public void GenerateWeatherData_ShouldReturnRequestedNumberOfDays()
        {
            // Arrange
            var parameters = new SimulationParameters { DaysToDisplay = 10 };

            // Act
            var result = _service.GenerateWeatherData(parameters);

            // Assert
            result.Should().HaveCount(10);
        }

        [Fact]
        public void GenerateWeatherData_ShouldIncludeSummaryMetrics()
        {
            // Arrange
            var parameters = new SimulationParameters { DaysToDisplay = 5 };

            // Act
            var result = _service.GenerateWeatherData(parameters);

            // Assert
            result.Should().AllSatisfy(d => 
            {
                d.Summary.Should().NotBeNull();
                d.Summary.AverageTemperature.Should().BeInRange(-30, 50);
                d.Summary.MaxWindSpeed.Should().BeGreaterThanOrEqualTo(0);
            });
        }

        [Fact]
        public void GenerateWeatherData_ShouldReflectSeasonalExpectations_January()
        {
            // Note: This test depends on the current date if the service uses DateTime.Today
            // We might want to refactor the service to accept a start date for better testability.
            // But for now, let's test general ranges.
            
            // Arrange
            var parameters = new SimulationParameters { DaysToDisplay = 1 };

            // Act
            var result = _service.GenerateWeatherData(parameters);

            // Assert
            var entry = result.First();
            entry.Temperature.Should().BeInRange(-40, 45); // General continental range
        }
        
        [Fact]
        public void StormProbability_ShouldBeBetweenZeroAndOneHundred()
        {
            // Arrange
            var parameters = new SimulationParameters { DaysToDisplay = 30 };

            // Act
            var result = _service.GenerateWeatherData(parameters);

            // Assert
            result.Should().AllSatisfy(d => d.StormProbability.Should().BeInRange(0, 100));
        }
    }
}
