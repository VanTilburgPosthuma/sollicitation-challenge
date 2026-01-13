namespace WeatherDashboard.Models
{
    public class SimulationParameters
    {
        public int DaysToDisplay { get; set; } = 30;
        public double TemperatureAmplitude { get; set; } = 10.0;
        public double AverageTemperature { get; set; } = 20.0;
        public double TimeScale { get; set; } = 1.0; // multiplier for frequency
    }
}
