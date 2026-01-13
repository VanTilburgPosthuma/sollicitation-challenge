# Weather Dashboard Simulation

An ASP.NET Core MVC application that simulates and visualizes weather patterns based on East European climate characteristics. This project demonstrates seasonal temperature cycles, random weather fluctuations, and various weather metrics.

## Features

- **Seasonal Weather Simulation**: Simulates temperatures using a seasonal cycle (coldest in January, hottest in July).
- **Interactive Dashboard**: View generated weather data for a specified number of days.
- **Dynamic Simulation**: Parameters like average temperature and display duration can be adjusted dynamically.
- **Weather Metrics**:
    - Temperature (with seasonal influence and random fluctuations)
    - Rainfall (with probability-based heavy bursts)
    - Wind Speed (with seasonal peaks and random gusts)
    - Heat Index calculation
    - Storm Probability heuristic
- **Unit Tests**: Comprehensive tests covering simulation logic and data integrity.

## Technologies Used

- **Framework**: ASP.NET Core 10.0 (MVC)
- **Frontend**: Razor Pages, JavaScript, jQuery, Bootstrap
- **Testing**: xUnit, FluentAssertions
- **IDE**: JetBrains Rider / Visual Studio

## Project Structure

- `WeatherDashboard/`: The main web application project.
    - `Controllers/`: MVC controllers (e.g., `DashboardController`).
    - `Models/`: Data models for weather data and simulation parameters.
    - `Services/`: Business logic for weather simulation.
    - `Views/`: Razor views for the user interface.
    - `wwwroot/`: Static assets (CSS, JS, libraries).
- `WeatherDashboard.Tests/`: Unit tests project.

## Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Installation & Running

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd solicitation-challenge
   ```

2. Build the solution:
   ```bash
   dotnet build
   ```

3. Run the application:
   ```bash
   dotnet run --project WeatherDashboard
   ```

4. Open your browser and navigate to `https://localhost:5001` (or the port specified in the terminal).

## Running Tests

To execute the unit tests:

```bash
dotnet test
```

The tests cover:
- Verification of the number of days generated.
- Integrity of summary metrics.
- Seasonal temperature range expectations.
- Validity of storm probability calculations.
