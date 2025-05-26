// Sample product data
const products = [
    {
        id: 1,
        name: "Fresh Organic Apples",
        category: "Fruits",
        price: 4.99,
        stock: 150,
        status: "active",
        rating: 4.8,
        image: "🍎"
    },
    {
        id: 2,
        name: "Farm Fresh Carrots",
        category: "Vegetables",
        price: 2.99,
        stock: 8,
        status: "low-stock",
        rating: 4.6,
        image: "🥕"
    },
    {
        id: 3,
        name: "Whole Milk",
        category: "Dairy",
        price: 3.49,
        stock: 45,
        status: "active",
        rating: 4.7,
        image: "🥛"
    },
    {
        id: 4,
        name: "Fresh Salmon Fillet",
        category: "Meat",
        price: 12.99,
        stock: 0,
        status: "inactive",
        rating: 4.9,
        image: "🐟"
    },
    {
        id: 5,
        name: "Artisan Bread",
        category: "Bakery",
        price: 5.99,
        stock: 25,
        status: "active",
        rating: 4.5,
        image: "🍞"
    }
];

// Render products table
// Render products table
function renderProducts(productsToRender = products) {
    const tbody = document.getElementById('productsTableBody');
    tbody.innerHTML = '';

    productsToRender.forEach(product => {
        const statusClass =
            product.status === 'active' ? 'status-active' :
                product.status === 'inactive' ? 'status-inactive' : 'status-low-stock';

        const statusText =
            product.status === 'active' ? 'Active' :
                product.status === 'inactive' ? 'Inactive' : 'Low Stock';

        const row = `
                    <tr>
                        <td>
                            <div class="form-check">
                                <input class="form-check-input" type="checkbox" value="${product.id}">
                            </div>
                        </td>
                        <td>
                            <div class="d-flex align-items-center">
                                <div class="product-image me-3">
                                    <span style="font-size: 32px;">${product.image}</span>
                                </div>
                                <div>
                                    <h6 class="mb-0">${product.name}</h6>
                                    <small class="text-muted">ID: #${product.id.toString().padStart(4, '0')}</small>
                                </div>
                            </div>
                        </td>
                        <td>${product.category}</td>
                        <td>$${product.price.toFixed(2)}</td>
                        <td>${product.stock}</td>
                        <td><span class="status-badge ${statusClass}">${statusText}</span></td>
                        <td>
                            <div class="d-flex align-items-center">
                                <iconify-icon icon="mdi:star" style="color: #FFC43F; margin-right: 4px;"></iconify-icon>
                                ${product.rating}
                            </div>
                        </td>
                        <td>
                            <button class="action-btn btn-view" title="View" onclick="viewProduct(${product.id})">
                                <iconify-icon icon="mdi:eye"></iconify-icon>
                            </button>
                            <button class="action-btn btn-edit" title="Edit" onclick="editProduct(${product.id})">
                                <iconify-icon icon="mdi:pencil"></iconify-icon>
                            </button>
                            <button class="action-btn btn-delete" title="Delete" onclick="deleteProduct(${product.id})">
                                <iconify-icon icon="mdi:delete"></iconify-icon>
                            </button>
                        </td>
                    </tr>
                `;
        tbody.innerHTML += row;
    });
}

// Product CRUD operations
function viewProduct(id) {
    const product = products.find(p => p.id === id);
    if (product) {
        alert(`Viewing product: ${product.name}\nCategory: ${product.category}\nPrice: ${product.price}\nStock: ${product.stock}`);
    }
}

function editProduct(id) {
    const product = products.find(p => p.id === id);
    if (product) {
        // Populate edit modal with product data
        document.getElementById('editProductModalLabel').textContent = `Edit ${product.name}`;
        const editModal = new bootstrap.Modal(document.getElementById('editProductModal'));
        editModal.show();
    }
}

function deleteProduct(id) {
    const product = products.find(p => p.id === id);
    if (product && confirm(`Are you sure you want to delete "${product.name}"?`)) {
        const index = products.findIndex(p => p.id === id);
        products.splice(index, 1);
        renderProducts();
        updateStats();
    }
}
// Add new product
function addProduct() {
    const form = document.getElementById('addProductForm');
    const formData = new FormData(form);

    const newProduct = {
        id: Math.max(...products.map(p => p.id)) + 1,
        name: document.getElementById('productName').value,
        category: document.getElementById('productCategory').value,
        price: parseFloat(document.getElementById('productPrice').value),
        stock: parseInt(document.getElementById('productStock').value),
        status: document.getElementById('productStatus').value,
        rating: (Math.random() * 2 + 3).toFixed(1), // Random rating between 3-5
        image: getCategoryEmoji(document.getElementById('productCategory').value)
    };

    products.push(newProduct);
    renderProducts();
    updateStats();

    // Reset form and close modal
    form.reset();
    const modal = bootstrap.Modal.getInstance(document.getElementById('addProductModal'));
    modal.hide();
}

function getCategoryEmoji(category) {
    const emojis = {
        'fruits': '🍎',
        'vegetables': '🥕',
        'dairy': '🥛',
        'meat': '🥩',
        'bakery': '🍞'
    };
    return emojis[category] || '📦';
}

// Update statistics
function updateStats() {
    const totalProducts = products.length;
    const lowStockProducts = products.filter(p => p.stock <= 10).length;
    const inactiveProducts = products.filter(p => p.status === 'inactive').length;
    const categories = [...new Set(products.map(p => p.category))].length;

    // Update stats display (you would update actual DOM elements here)
    console.log('Stats updated:', { totalProducts, lowStockProducts, inactiveProducts, categories });
}

// Search and filter functionality
function filterProducts() {
    const searchTerm = document.getElementById('searchInput').value.toLowerCase();
    const categoryFilter = document.getElementById('categoryFilter').value;
    const statusFilter = document.getElementById('statusFilter').value;

    let filteredProducts = products.filter(product => {
        const matchesSearch = product.name.toLowerCase().includes(searchTerm) ||
            product.category.toLowerCase().includes(searchTerm);
        const matchesCategory = !categoryFilter || product.category.toLowerCase() === categoryFilter;
        const matchesStatus = !statusFilter ||
            (statusFilter === 'low-stock' && product.stock <= 10) ||
            (statusFilter !== 'low-stock' && product.status === statusFilter);

        return matchesSearch && matchesCategory && matchesStatus;
    });

    renderProducts(filteredProducts);
}

/ Image upload functionality
function setupImageUpload() {
    const uploadArea = document.getElementById('imageUploadArea');
    const fileInput = document.getElementById('productImages');
    const preview = document.getElementById('imagePreview');

    uploadArea.addEventListener('click', () => fileInput.click());

    uploadArea.addEventListener('dragover', (e) => {
        e.preventDefault();
        uploadArea.classList.add('dragover');
    });

    uploadArea.addEventListener('dragleave', (e) => {
        e.preventDefault();
        uploadArea.classList.remove('dragover');
    });

    uploadArea.addEventListener('drop', (e) => {
        e.preventDefault();
        uploadArea.classList.remove('dragover');
        const files = e.dataTransfer.files;
        handleFiles(files);
    });

    fileInput.addEventListener('change', (e) => {
        handleFiles(e.target.files);
    });

    function handleFiles(files) {
        preview.innerHTML = '';
        Array.from(files).forEach((file, index) => {
            if (file.type.startsWith('image/')) {
                const reader = new FileReader();
                reader.onload = (e) => {
                    const col = document.createElement('div');
                    col.className = 'col-md-3 mb-2';
                    col.innerHTML = `
                                <div class="position-relative">
                                    <img src="${e.target.result}" class="img-fluid rounded" style="height: 100px; object-fit: cover;">
                                    <button type="button" class="btn btn-danger btn-sm position-absolute top-0 end-0 m-1" onclick="this.parentElement.parentElement.remove()">
                                        <iconify-icon icon="mdi:close"></iconify-icon>
                                    </button>
                                </div>
                            `;
                    preview.appendChild(col);
                };
                reader.readAsDataURL(file);
            }
        });
    }
}

// Image upload functionality
function setupImageUpload() {
    const uploadArea = document.getElementById('imageUploadArea');
    const fileInput = document.getElementById('productImages');
    const preview = document.getElementById('imagePreview');

    uploadArea.addEventListener('click', () => fileInput.click());

    uploadArea.addEventListener('dragover', (e) => {
        e.preventDefault();
        uploadArea.classList.add('dragover');
    });

    uploadArea.addEventListener('dragleave', (e) => {
        e.preventDefault();
        uploadArea.classList.remove('dragover');
    });

    uploadArea.addEventListener('drop', (e) => {
        e.preventDefault();
        uploadArea.classList.remove('dragover');
        const files = e.dataTransfer.files;
        handleFiles(files);
    });

    fileInput.addEventListener('change', (e) => {
        handleFiles(e.target.files);
    });

    function handleFiles(files) {
        preview.innerHTML = '';
        Array.from(files).forEach((file, index) => {
            if (file.type.startsWith('image/')) {
                const reader = new FileReader();
                reader.onload = (e) => {
                    const col = document.createElement('div');
                    col.className = 'col-md-3 mb-2';
                    col.innerHTML = `
                                <div class="position-relative">
                                    <img src="${e.target.result}" class="img-fluid rounded" style="height: 100px; object-fit: cover;">
                                    <button type="button" class="btn btn-danger btn-sm position-absolute top-0 end-0 m-1" onclick="this.parentElement.parentElement.remove()">
                                        <iconify-icon icon="mdi:close"></iconify-icon>
                                    </button>
                                </div>
                            `;
                    preview.appendChild(col);
                };
                reader.readAsDataURL(file);
            }
        });
    }
}

// Export functionality
function exportProducts() {
    const csvContent = "data:text/csv;charset=utf-8," +
        "ID,Name,Category,Price,Stock,Status,Rating\n" +
        products.map(p => `${p.id},"${p.name}",${p.category},${p.price},${p.stock},${p.status},${p.rating}`).join("\n");

    const encodedUri = encodeURI(csvContent);
    const link = document.createElement("a");
    link.setAttribute("href", encodedUri);
    link.setAttribute("download", "products.csv");
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

// Initialize the application
document.addEventListener('DOMContentLoaded', function() {
    renderProducts();
    setupImageUpload();
    setupSelectAll();
    setupSidebarToggle();

    // Event listeners
    document.getElementById('searchInput').addEventListener('input', filterProducts);
    document.getElementById('categoryFilter').addEventListener('change', filterProducts);
    document.getElementById('statusFilter').addEventListener('change', filterProducts);
    document.getElementById('saveProduct').addEventListener('click', addProduct);

    // Export button functionality
    const exportButtons = document.querySelectorAll('button:contains("Export")');
    exportButtons.forEach(btn => {
        if (btn.textContent.includes('Export')) {
            btn.addEventListener('click', exportProducts);
        }
    });
});