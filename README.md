# Weav-App 🧵

Weav-App is a modular, role-based e-commerce-style application designed for managing users, products, categories, and authentication. Built using **ASP.NET Core MVC**, **Supabase** as the backend, and integrated with **AutoMapper**, **Authentication Cookies**, and **DTO-based data handling**, the project focuses on clean architecture, maintainability, and user experience.

---

## 🚀 Features

- 🧑 User Registration & Login with role-based access
- 🔒 Cookie-based authentication
- 🛍️ Product listing, filtering, and category management
- 🧩 Modular architecture with DTOs, ViewModels, and AutoMapper
- 📦 Supabase integration for live database operations
- 📄 Dynamic UI powered by Razor Views and Bootstrap
- 🔍 Admin product filtering by status, category, and keyword
- ☁️ Dependency Injection and service-based structure

---

## 🧠 Technologies Used

- **ASP.NET Core MVC**
- **Supabase** (PostgreSQL backend)
- **AutoMapper**
- **Entity Models + DTOs**
- **Razor Pages / Views**
- **Bootstrap 5**
- **Authentication Cookies**
- **LINQ & async/await**

---

## 📁 Project Structure

```
Weav-App/
├── Controllers/
│   ├── AdminController.cs
│   ├── RegistrationController.cs
├── DTOs/
│   └── Entities/
├── Services/
│   ├── Auth/
│   ├── Interface/
│   └── Product/
├── ViewModels/
├── Views/
├── wwwroot/
├── Program.cs
├── appsettings.json
```
1. **Clone the repository**
   ```bash
   git clone https://github.com/andrea-19d/Weav-App.git
   cd Weav-App

## 📌 TODOs / Future Features
JWT authentication for API clients

Google OAuth login

Email confirmation & password reset

Full CRUD for categories & users (admin panel)

Unit tests for services

---

## 🤝 Contribution
Pull requests and ideas are welcome!
Feel free to fork and build on top of this for your own learning or commercial projects.

---

## 📄 License
MIT License – use freely, but give credit if inspired!
=