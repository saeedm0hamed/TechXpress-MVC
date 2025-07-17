
# TechXpress

**TechXpress** is a modern, full-featured e-commerce web application for discovering and purchasing the latest tech gadgets. Built with ASP.NET Core, Entity Framework Core, and Razor Pages, it provides a robust, scalable, and user-friendly platform for both customers and administrators.

---

## 🚀 Features

### 🛒 User-Facing Features
- **Product Catalog**: Browse, search, and filter products by category, price range, and name.
- **Product Details**: View detailed product information, including images, descriptions, price, stock status, and reviews.
- **Add to Cart**: Seamlessly add products to your cart (with authentication check and graceful handling of login state).
- **Cart Management**: View, update, and remove items from your cart.
- **Checkout**: Place orders with address, payment method, and order summary.
- **User Authentication**: Register, login, logout, and manage your account securely.
- **Email Confirmation**: Receive confirmation emails after registration (SMTP integration).
- **Responsive UI**: Modern, mobile-friendly design with grid and list views for products.

### 🛠️ Admin Features
- **Product Management**: Add, edit, and delete products with image upload and validation.
- **Category Management**: Organize products into categories.
- **Stock Management**: Track and update product stock levels.
- **Order Management**: View and manage customer orders.
- **Reports**: Access sales and inventory reports (extensible).

---

## 🏗️ Project Structure

```
TechXpress/
├── TechXpress.Web/         # ASP.NET Core Web (UI, controllers, views, identity)
├── TechXpress.Data/        # Data access, models, repositories, migrations
├── TechXpress.Services/    # Business logic, email sender, file service, utilities
```

---

## 💡 Technologies & Techniques

- **ASP.NET Core 9.0**: Modern, high-performance web framework.
- **Entity Framework Core**: ORM for SQL Server, code-first migrations.
- **Razor Pages & MVC**: Clean separation of concerns, reusable views.
- **ASP.NET Core Identity**: Secure authentication, registration, and user management.
- **SMTP Email Integration**: Custom `EmailSender` service for registration and notifications.
- **Dependency Injection**: Modular, testable architecture.
- **Repository & Unit of Work Patterns**: Clean data access and business logic separation.
- **Bootstrap 5 & FontAwesome**: Responsive, attractive UI.
- **Toastr**: User-friendly notifications.
- **AJAX/Fetch API**: Smooth, dynamic cart and product interactions.
- **Configuration via appsettings.json**: Centralized settings for DB, SMTP, etc.

---

## ⚙️ Configuration

- **Database**: SQL Server (connection string in `appsettings.json`)
- **Email (SMTP)**: Configure your SMTP provider in `TechXpress.Web/appsettings.json`:
  ```json
  "Smtp": {
    "Host": "smtp.example.com",
    "Port": 587,
    "User": "your-smtp-username",
    "Pass": "your-smtp-password",
    "FromEmail": "no-reply@example.com"
  }
  ```

---

## 🏁 Getting Started

1. **Clone the repository**
2. **Configure your database and SMTP settings** in `appsettings.json`
3. **Run database migrations** (if needed)
4. **Build and run the solution**
5. **Register a new user and start shopping!**

---

## 📂 Notable Files & Folders

- `TechXpress.Web/Controllers/` - Main controllers for products, cart, admin, etc.
- `TechXpress.Web/Views/` - Razor views for all pages and features.
- `TechXpress.Data/Models/` - Core domain models: Product, Cart, Order, etc.
- `TechXpress.Data/Repositories/` - Data access logic.
- `TechXpress.Services/EmailSender.cs` - SMTP email sender implementation.

---

## 🙌 Contributing

Pull requests and suggestions are welcome! Please open an issue to discuss your ideas or report bugs.

---

## 📜 License

This project is licensed under the MIT License.

---

**TechXpress** — Your one-stop shop for the latest in tech!

---

Let me know if you want to include setup instructions, screenshots, or any other details!

