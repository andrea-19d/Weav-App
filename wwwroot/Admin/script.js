

// Sales Chart
const salesCtx = document.getElementById('salesChart').getContext('2d');
new Chart(salesCtx, {
    type: 'line',
    data: {
        labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
        datasets: [{
            label: 'Sales',
            data: [12000, 19000, 15000, 25000, 22000, 30000],
            borderColor: '#FFC43F',
            backgroundColor: 'rgba(255, 196, 63, 0.1)',
            tension: 0.4,
            fill: true
        }]
    },
    options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                display: false
            }
        },
        scales: {
            y: {
                beginAtZero: true,
                grid: {
                    color: '#f8f9fa'
                }
            },
            x: {
                grid: {
                    color: '#f8f9fa'
                }
            }
        }
    }
});

// Category Chart
const categoryCtx = document.getElementById('categoryChart').getContext('2d');
new Chart(categoryCtx, {
    type: 'doughnut',
    data: {
        labels: ['Fruits', 'Vegetables', 'Dairy', 'Bakery', 'Meat'],
        datasets: [{
            data: [30, 25, 20, 15, 10],
            backgroundColor: [
                '#FFC43F',
                '#6995B1',
                '#a3be4c',
                '#FFEADA',
                '#EEF5E4'
            ],
            borderWidth: 0
        }]
    },
    options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                position: 'bottom',
                labels: {
                    padding: 20,
                    usePointStyle: true
                }
            }
        }
    }
});

// Set chart container heights
document.getElementById('salesChart').parentElement.style.height = '300px';
document.getElementById('categoryChart').parentElement.style.height = '300px';