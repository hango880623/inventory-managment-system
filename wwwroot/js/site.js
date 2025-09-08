// Custom JavaScript for Inventory Management System

// Initialize tooltips
document.addEventListener('DOMContentLoaded', function() {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
});

// Show toast notifications
function showToast(message, type = 'info') {
    if (typeof toastr !== 'undefined') {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        switch (type) {
            case 'success':
                toastr.success(message);
                break;
            case 'error':
                toastr.error(message);
                break;
            case 'warning':
                toastr.warning(message);
                break;
            default:
                toastr.info(message);
        }
    }
}

// Show success message from TempData
function showSuccessMessage(message) {
    if (message) {
        showToast(message, 'success');
    }
}

// Show error message from TempData
function showErrorMessage(message) {
    if (message) {
        showToast(message, 'error');
    }
}

// Confirm delete action
function confirmDelete(id, name, actionUrl) {
    if (confirm(`Are you sure you want to delete ${name}? This action cannot be undone.`)) {
        const form = document.createElement('form');
        form.method = 'POST';
        form.action = actionUrl;
        
        const token = document.querySelector('input[name="__RequestVerificationToken"]');
        if (token) {
            form.appendChild(token.cloneNode());
        }
        
        document.body.appendChild(form);
        form.submit();
    }
}

// Auto-hide alerts after 5 seconds
document.addEventListener('DOMContentLoaded', function() {
    const alerts = document.querySelectorAll('.alert:not(.alert-permanent)');
    alerts.forEach(function(alert) {
        setTimeout(function() {
            const bsAlert = new bootstrap.Alert(alert);
            bsAlert.close();
        }, 5000);
    });
});

// Form validation enhancement
function enhanceFormValidation() {
    const forms = document.querySelectorAll('form[data-validate]');
    forms.forEach(function(form) {
        form.addEventListener('submit', function(event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }
            form.classList.add('was-validated');
        });
    });
}

// Initialize form validation
document.addEventListener('DOMContentLoaded', function() {
    enhanceFormValidation();
});

// Loading state for buttons
function setButtonLoading(button, loading = true) {
    if (loading) {
        button.disabled = true;
        button.dataset.originalText = button.innerHTML;
        button.innerHTML = '<span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>Loading...';
    } else {
        button.disabled = false;
        button.innerHTML = button.dataset.originalText || button.innerHTML;
    }
}

// Auto-save form data to localStorage
function autoSaveForm(formId) {
    const form = document.getElementById(formId);
    if (!form) return;

    const inputs = form.querySelectorAll('input, select, textarea');
    
    // Load saved data
    inputs.forEach(function(input) {
        const savedValue = localStorage.getItem(`${formId}_${input.name}`);
        if (savedValue && !input.value) {
            input.value = savedValue;
        }
    });

    // Save data on input change
    inputs.forEach(function(input) {
        input.addEventListener('input', function() {
            localStorage.setItem(`${formId}_${input.name}`, input.value);
        });
    });

    // Clear saved data on successful submit
    form.addEventListener('submit', function() {
        inputs.forEach(function(input) {
            localStorage.removeItem(`${formId}_${input.name}`);
        });
    });
}

// Table row highlighting
function highlightTableRow(tableId) {
    const table = document.getElementById(tableId);
    if (!table) return;

    const rows = table.querySelectorAll('tbody tr');
    rows.forEach(function(row) {
        row.addEventListener('mouseenter', function() {
            this.classList.add('table-active');
        });
        
        row.addEventListener('mouseleave', function() {
            this.classList.remove('table-active');
        });
    });
}

// Initialize table highlighting
document.addEventListener('DOMContentLoaded', function() {
    const tables = document.querySelectorAll('.table');
    tables.forEach(function(table) {
        highlightTableRow(table.id);
    });
});

// Search functionality for tables
function initializeTableSearch(inputId, tableId) {
    const searchInput = document.getElementById(inputId);
    const table = document.getElementById(tableId);
    
    if (!searchInput || !table) return;

    searchInput.addEventListener('input', function() {
        const searchTerm = this.value.toLowerCase();
        const rows = table.querySelectorAll('tbody tr');
        
        rows.forEach(function(row) {
            const text = row.textContent.toLowerCase();
            if (text.includes(searchTerm)) {
                row.style.display = '';
            } else {
                row.style.display = 'none';
            }
        });
    });
}

// Export table to CSV
function exportTableToCSV(tableId, filename) {
    const table = document.getElementById(tableId);
    if (!table) return;

    let csv = [];
    const rows = table.querySelectorAll('tr');
    
    rows.forEach(function(row) {
        const cells = row.querySelectorAll('td, th');
        const rowData = [];
        
        cells.forEach(function(cell) {
            rowData.push('"' + cell.textContent.replace(/"/g, '""') + '"');
        });
        
        csv.push(rowData.join(','));
    });
    
    const csvContent = csv.join('\n');
    const blob = new Blob([csvContent], { type: 'text/csv' });
    const url = window.URL.createObjectURL(blob);
    
    const link = document.createElement('a');
    link.href = url;
    link.download = filename || 'export.csv';
    link.click();
    
    window.URL.revokeObjectURL(url);
}

// Print table
function printTable(tableId) {
    const table = document.getElementById(tableId);
    if (!table) return;

    const printWindow = window.open('', '_blank');
    printWindow.document.write(`
        <html>
            <head>
                <title>Print Table</title>
                <style>
                    body { font-family: Arial, sans-serif; }
                    table { border-collapse: collapse; width: 100%; }
                    th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }
                    th { background-color: #f2f2f2; }
                </style>
            </head>
            <body>
                ${table.outerHTML}
            </body>
        </html>
    `);
    printWindow.document.close();
    printWindow.print();
}

// Initialize all custom functionality
document.addEventListener('DOMContentLoaded', function() {
    // Show messages from TempData
    const successMessage = document.querySelector('[data-success-message]');
    if (successMessage) {
        showSuccessMessage(successMessage.dataset.successMessage);
    }
    
    const errorMessage = document.querySelector('[data-error-message]');
    if (errorMessage) {
        showErrorMessage(errorMessage.dataset.errorMessage);
    }
    
    // Initialize auto-save for forms
    const forms = document.querySelectorAll('form[data-auto-save]');
    forms.forEach(function(form) {
        autoSaveForm(form.id);
    });
});
