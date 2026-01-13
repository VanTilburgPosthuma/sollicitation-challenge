using System;

namespace WeatherDashboard.Models
{
    public class WeatherData
    {
        public DateTime Date { get; set; }
        public double Temperature { get; set; } // in Celsius
        public double Rainfall { get; set; } // in mm
        public double WindSpeed { get; set; } // in km/h
        
        public double HeatIndex { get; set; }
        public double StormProbability { get; set; } // 0 to 100

        public WeatherMetrics Summary { get; set; }
    }

    public class WeatherMetrics
    {
        public double AverageTemperature { get; set; }
        public double TotalRainfall { get; set; }
        public double MaxWindSpeed { get; set; }
    }
}
