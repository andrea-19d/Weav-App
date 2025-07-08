

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

class NotificationButton {
    constructor(buttonId, badgeId) {
        this.button = document.getElementById(buttonId);
        this.badge = document.getElementById(badgeId);
        this.currentCount = 0;
        this.init();
    }

    init() {
        if (this.button) {
            this.button.addEventListener('click', () => this.handleClick());
        }
        this.setupSignalR();
    }

    handleClick() {
        // Make AJAX call to create new order
        fetch('/Orders/CreateNewOrder', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            }
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    this.updateBadge(data.unconfirmedCount);
                    this.showSuccessMessage();
                } else {
                    this.showErrorMessage(data.message);
                }
            })
            .catch(error => {
                console.error('Error:', error);
                this.showErrorMessage('An error occurred');
            });
    }

    updateBadge(count) {
        this.currentCount = count;

        if (this.badge) {
            if (count > 0) {
                this.badge.textContent = count > 99 ? '99+' : count;
                this.badge.style.display = 'flex';

                // Add pulse animation for high counts
                if (count > 5) {
                    this.badge.classList.add('pulse');
                } else {
                    this.badge.classList.remove('pulse');
                }
            } else {
                this.badge.style.display = 'none';
            }
        }
    }

    setupSignalR() {
        if (typeof signalR !== 'undefined') {
            const connection = new signalR.HubConnectionBuilder()
                .withUrl("/ordersHub")
                .build();

            connection.start().then(() => {
                console.log("SignalR Connected");
            });

            connection.on("UpdateOrderCount", (count) => {
                this.updateBadge(count);
            });
        }
    }

    showSuccessMessage() {
        // Implement your success notification
        console.log('Order created successfully');
    }

    showErrorMessage(message) {
        // Implement your error notification
        console.error('Error:', message);
    }
}

// Auto-populate hidden fields
document.getElementById('userIp').value = 'Auto-detected'; // Would be set by backend
document.getElementById('registerDate').value = new Date().toISOString();

// Photo preview functionality
document.getElementById('userPhoto').addEventListener('change', function(e) {
    const file = e.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function(e) {
            const preview = document.getElementById('photoPreview');
            preview.src = e.target.result;
            preview.style.display = 'block';
        };
        reader.readAsDataURL(file);
    }
});

// Password toggle functionality
function togglePassword() {
    const passwordInput = document.getElementById('password');
    const toggleIcon = document.getElementById('passwordToggle');

    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        toggleIcon.classList.remove('fa-eye');
        toggleIcon.classList.add('fa-eye-slash');
    } else {
        passwordInput.type = 'password';
        toggleIcon.classList.remove('fa-eye-slash');
        toggleIcon.classList.add('fa-eye');
    }
}

// Password strength checker
document.getElementById('password').addEventListener('input', function() {
    const password = this.value;
    const strengthBar = document.getElementById('strengthBar');
    const strengthText = document.getElementById('strengthText');

    // Remove all strength classes
    strengthBar.className = 'password-strength-bar';

    if (password.length === 0) {
        strengthText.textContent = 'Enter password';
        return;
    }

    let strength = 0;
    if (password.length >= 8) strength++;
    if (/[a-z]/.test(password)) strength++;
    if (/[A-Z]/.test(password)) strength++;
    if (/[0-9]/.test(password)) strength++;
    if (/[^A-Za-z0-9]/.test(password)) strength++;

    switch (strength) {
        case 1:
        case 2:
            strengthBar.classList.add('strength-weak');
            strengthText.textContent = 'Weak';
            break;
        case 3:
            strengthBar.classList.add('strength-fair');
            strengthText.textContent = 'Fair';
            break;
        case 4:
            strengthBar.classList.add('strength-good');
            strengthText.textContent = 'Good';
            break;
        case 5:
            strengthBar.classList.add('strength-strong');
            strengthText.textContent = 'Strong';
            break;
    }
});

// Form validation and submission
document.getElementById('registrationForm').addEventListener('submit', function(e) {
    e.preventDefault();

    // Reset validation
    const inputs = this.querySelectorAll('.form-control, .form-select');
    inputs.forEach(input => {
        input.classList.remove('is-invalid');
    });

    let isValid = true;

    // Validate required fields
    inputs.forEach(input => {
        if (input.hasAttribute('required') && !input.value.trim()) {
            input.classList.add('is-invalid');
            isValid = false;
        }
    });

    // Validate email format
    const email = document.getElementById('email');
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email.value)) {
        email.classList.add('is-invalid');
        isValid = false;
    }

    // Validate password confirmation
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;
    if (password !== confirmPassword) {
        document.getElementById('confirmPassword').classList.add('is-invalid');
        isValid = false;
    }

    if (isValid) {
        // Show loading state
        const submitBtn = document.getElementById('submitBtn');
        submitBtn.classList.add('btn-loading');
        submitBtn.disabled = true;

        // Simulate form submission
        setTimeout(() => {
            alert('Admin account created successfully!');
            submitBtn.classList.remove('btn-loading');
            submitBtn.disabled = false;
            // In real implementation, you would submit to your backend here
        }, 2000);
    }
});

// Real-time validation feedback
document.querySelectorAll('.form-control, .form-select').forEach(input => {
    input.addEventListener('blur', function() {
        if (this.hasAttribute('required') && !this.value.trim()) {
            this.classList.add('is-invalid');
        } else {
            this.classList.remove('is-invalid');
        }
    });
});

