import  'https://cdn.jsdelivr.net/npm/chart.js';

var _chart;
var _renderTimer;
var _maxDataPoints;

export function InitializeChart(rerenderFrequency, maxDataPoints) {
    var chart_element = document.getElementById('demo-chart');

    _chart = new Chart(chart_element, {
        type: 'line',
        data: {
            labels: [],
            datasets: [
                {
                    label: 'live data graph',
                    data: [],
                    borderWidth: 1
                }
            ]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: false
                }
            },
            animation: false
        }
    });

    UpdateRerenderFrequency(rerenderFrequency);
    _maxDataPoints = maxDataPoints;
}

export function UpdateMaxDataPoints(maxDataPoints) {
    _maxDataPoints = maxDataPoints;
    _chart.data.labels = _chart.data.labels.slice(-(_maxDataPoints));
    _chart.data.datasets[0].data = _chart.data.datasets[0].data.slice(-(_maxDataPoints));
}

export function UpdateRerenderFrequency(rerenderFrequency) {
    try {
        clearInterval(_renderTimer)
    } catch (ex) {
        console.log(ex);
    }

    _renderTimer = setInterval(() => {
        _chart.update();
    }, rerenderFrequency);
}

export function AddDataPoint(label, value) {
    _chart.data.labels.push(label);
    _chart.data.datasets[0].data.push(value);
    _chart.data.labels = _chart.data.labels.slice(-(_maxDataPoints));
    _chart.data.datasets[0].data = _chart.data.datasets[0].data.slice(-(_maxDataPoints));
}

export function ResetDataPoints() {
    _chart.data.labels = [];
    _chart.data.datasets[0].data = [];
}