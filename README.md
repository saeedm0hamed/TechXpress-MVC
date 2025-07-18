
# 🛒 TechXpress

**TechXpress** is a modern, scalable, fully-functional e-commerce web application for discovering and purchasing the latest tech gadgets.
Built as [my](https://www.linkedin.com/posts/saeedm0hamed_%D8%A7%D9%84%D8%AD%D9%85%D8%AF%D9%84%D9%84%D9%87-%D8%A7%D9%84%D8%B0%D9%8A-%D8%B9%D9%84%D9%85%D9%86%D8%A7-%D9%85%D8%A7-%D9%84%D9%85-%D9%86%D9%83%D9%86-%D9%86%D8%B9%D9%84%D9%85-%D9%88%D9%83%D8%A7%D9%86-activity-7349105488690671616-4Dmo?utm_source=share&utm_medium=member_desktop&rcm=ACoAAEdvbmgBbqKej5BdhXCdZ4zffCgiTeg8u2c) graduation project for [Digital Egypt Pioneers Initiative - DEPI](https://depi.gov.eg/) by [Ministry of Communications and Information Technology (MCIT), Egypt](https://www.mcit.gov.eg/)!, it provides a robust, scalable, and user-friendly platform for both customers and administrators.

## 🖥️ Interface Preview
<div align="center">
  <img src="/hero.png">
</div>

## 💡 Technologies & Techniques
<div align="center">

#### Frontend & UI
[![Tech Stack](https://skillicons.dev/icons?i=html,css,js,bootstrap)](https://skillicons.dev)

#### Backend & Database
<div align="center">
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" height="40" alt="csharp logo"  />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" height="40" alt="dotnetcore logo"  />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain.svg" height="40" alt="microsoftsqlserver logo"  />
</div>

#### Development Tools
[![Tech Stack](https://skillicons.dev/icons?i=git,github,visualstudio)](https://skillicons.dev)

</div>

- **ASP.NET Core 9.0**: Modern, high-performance web framework.
- **Entity Framework Core**: ORM for SQL Server, code-first migrations.
- **N-tier architecture & MVC**: Clean separation of concerns.
- **Dependency Injection**: Modular, testable architecture.
- **Repository & Unit of Work Patterns**: Clean data access and business logic separation.
- **Identity**: Secure authentication, registration, and user management.
- **Bootstrap 5 & FontAwesome & Toastr & AJAX**: Responsive, attractive UI, and smooth, dynamic interactions.

## 🚀 Features

### 👤 User-Facing Features
<table>
<tr>
<td align="center"><h3>🛍️</h3></td>
<td><b>Product Catalog</b><br>Browse, search, and filter products by category, price range, and name</td>
<td align="center"><h3>📄</h3></td>
<td><b>Product Details</b><br>View images, descriptions, prices, stock, and user reviews</td>
</tr>
<tr>
<td align="center"><h3>➕</h3></td>
<td><b>Add to Cart</b><br>Add products with auth checks and smooth guest handling</td>
<td align="center"><h3>🛒</h3></td>
<td><b>Cart Management</b><br>View, update, and remove items easily from your cart</td>
</tr>
<tr>
<td align="center"><h3>💳</h3></td>
<td><b>Checkout</b><br>Place orders with address input, payment method, and summary</td>
<td align="center"><h3>🔐</h3></td>
<td><b>Authentication</b><br>Register, login, logout, and manage accounts securely</td>
</tr>
<tr>
<td align="center"><h3>📧</h3></td>
<td><b>Email Confirmation</b><br>Receive confirmation emails after signing up</td>
<td align="center"><h3>📱</h3></td>
<td><b>Responsive UI</b><br>Mobile-friendly layout with grid and list views</td>
</tr>
</table>


</div>



### 🛠️ Admin Features
<table>
<tr>
<td align="center"><h3>📦</h3></td>
<td><b>Product Management</b><br>Add, edit, and delete products with image upload and validation</td>
<td align="center"><h3>🗂️</h3></td>
<td><b>Category Management</b><br>Organize products into logical categories</td>
</tr>
<tr>
<td align="center"><h3>📊</h3></td>
<td><b>Stock Management</b><br>Track and update product stock levels in real-time</td>
<td align="center"><h3>📋</h3></td>
<td><b>Order Management</b><br>View, process, and manage customer orders</td>
</tr>
</table>

</div>


## 🏗️ Project Structure
```
📂 TechXpress/
├── 📂 TechXpress.Web/         # ASP.NET Core Web (UI, controllers, views, identity)
├── 📂 TechXpress.Data/        # Data access, models, repositories, migrations
├── 📂 TechXpress.Services/    # Business logic, email sender, file service, utilities
```

## 🏁 Getting Started
1. Clone the repository
   ```bash
   git clone https://github.com/saeedm0hamed/TechXpress-MVC.git
   ```

2. Navigate to the project directory
   ```bash
   cd TechXpress
   ```

3. Restore dependencies
   ```bash
   dotnet restore
   ```

4. Configure your database and SMTP settings in `appsettings.json`

5. Apply migrations
   ```bash
   dotnet ef database update
   ```

6. Run the application
   ```bash
   dotnet run
   ```

<div align="center">
  <p>
    <a href="https://github.com/saeedm0hamed/"><img src="https://img.shields.io/badge/GitHub-Profile-181717?style=flat&logo=github&logoColor=white" alt="GitHub"></a>
    <a href="https://www.linkedin.com/in/saeedm0hamed/"><img src="https://custom-icon-badges.demolab.com/badge/LinkedIn-Profile-0A66C2?logo=linkedin-white&logoColor=fffwhite" alt="LinkedIn"></a>
    <a href="https://facebook.com/saeedm0hamed/"><img src="https://img.shields.io/badge/Facebook-Profile-%231877F2.svg?logo=Facebook&logoColor=white" alt="Facebook"></a>
    <a href="mailto:saeedmohamed.fs@gmail.com"><img src="https://img.shields.io/badge/Contact-Email-EA4335?style=flat&logo=gmail&logoColor=white" alt="Email"></a>
  </p>
</div>

