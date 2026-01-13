let weatherChart;
let windChart;

function initializeDashboard(data) {
    updateCharts(data);
    
    document.getElementById('simulationForm').addEventListener('submit', function (e) {
        e.preventDefault();
        
        const formData = new FormData(this);
        const params = {};
        formData.forEach((value, key) => params[key] = value);
        
        fetch('/Dashboard/Simulate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            },
            body: new URLSearchParams(params)
        })
        .then(response => response.json())
        .then(data => {
            updateCharts(data);
            updateMetrics(data);
        });
    });
}

function updateCharts(data) {
    const labels = data.map(d => new Date(d.date).toLocaleDateString());
    const temperatures = data.map(d => d.temperature);
    const rainfalls = data.map(d => d.rainfall);
    const windSpeeds = data.map(d => d.windSpeed);

    if (weatherChart) {
        weatherChart.data.labels = labels;
        weatherChart.data.datasets[0].data = temperatures;
        weatherChart.data.datasets[1].data = rainfalls;
        weatherChart.update();
    } else {
        const ctx = document.getElementById('weatherChart').getContext('2d');
        weatherChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [
                    {
                        label: 'Temperature (°C)',
                        data: temperatures,
                        borderColor: 'rgb(255, 99, 132)',
                        backgroundColor: 'rgba(255, 99, 132, 0.2)',
                        yAxisID: 'y',
                    },
                    {
                        label: 'Rainfall (mm)',
                        data: rainfalls,
                        borderColor: 'rgb(54, 162, 235)',
                        backgroundColor: 'rgba(54, 162, 235, 0.2)',
                        type: 'bar',
                        yAxisID: 'y1',
                    }
                ]
            },
            options: {
                responsive: true,
                interaction: {
                    mode: 'index',
                    intersect: false,
                },
                scales: {
                    y: {
                        type: 'linear',
                        display: true,
                        position: 'left',
                        title: { display: true, text: 'Temperature (°C)' }
                    },
                    y1: {
                        type: 'linear',
                        display: true,
                        position: 'right',
                        grid: { drawOnChartArea: false },
                        title: { display: true, text: 'Rainfall (mm)' }
                    },
                }
            }
        });
    }

    if (windChart) {
        windChart.data.labels = labels;
        windChart.data.datasets[0].data = windSpeeds;
        windChart.update();
    } else {
        const ctxWind = document.getElementById('windChart').getContext('2d');
        windChart = new Chart(ctxWind, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Wind Speed (km/h)',
                    data: windSpeeds,
                    borderColor: 'rgb(75, 192, 192)',
                    tension: 0.1,
                    fill: true,
                    backgroundColor: 'rgba(75, 192, 192, 0.1)'
                }]
            },
            options: {
                responsive: true,
                scales: {
                    y: {
                        beginAtZero: true,
                        title: { display: true, text: 'Wind Speed (km/h)' }
                    }
                }
            }
        });
    }
}

function updateMetrics(data) {
    if (!data.length) return;
    
    const latest = data[data.length - 1];
    const summary = latest.summary;
    
    if (summary) {
        document.getElementById('avgTempText').innerText = `${summary.averageTemperature.toFixed(1)} °C`;
        document.getElementById('totalRainText').innerText = `${summary.totalRainfall.toFixed(1)} mm`;
        document.getElementById('maxWindText').innerText = `${summary.maxWindSpeed.toFixed(1)} km/h`;
    }
    document.getElementById('stormProbText').innerText = `${latest.stormProbability}%`;
}
