<!-- Badges -->
<p align="center">
  <!-- Version -->
  <a href="https://github.com/nhave/Swagger.Bootstrap/releases">
    <img src="https://img.shields.io/github/v/release/nhave/Swagger.Bootstrap?style=for-the-badge" alt="Latest Release">
  </a>
  <!-- License -->
  <a href="https://github.com/nhave/Swagger.Bootstrap/blob/main/LICENSE">
    <img src="https://img.shields.io/github/license/nhave/Swagger.Bootstrap?style=for-the-badge" alt="License">
  </a>
  <!-- Issues -->
  <a href="https://github.com/nhave/Swagger.Bootstrap/issues">
    <img src="https://img.shields.io/github/issues/nhave/Swagger.Bootstrap?style=for-the-badge" alt="Open Issues">
  </a>
  <!-- Stars -->
  <a href="https://github.com/nhave/Swagger.Bootstrap/stargazers">
    <img src="https://img.shields.io/github/stars/nhave/Swagger.Bootstrap?style=for-the-badge" alt="Stars">
  </a>
</p>


# Swagger.Bootstrap

**Swagger.Bootstrap** is an add-on for [Swagger UI](https://swagger.io/tools/swagger-ui/) that gives it a modern [Bootstrap](https://getbootstrap.com/) look and feel.  
It also adds a simple theme switcher so users can toggle between **light** and **dark mode**.

## ✨ Features
- Bootstrap-inspired styling for Swagger UI
- Built-in theme switcher (light/dark)
- Easy integration with ASP.NET Core Swagger setup
- No npm or package manager required

## 📂 Getting Started

### 1. Download the files
Copy the provided `swagger.bootstrap.min.css` and `swagger.bootstrap.min.js` files into your project, for example under:
wwwroot


### 2. Inject into Swagger UI in ASP.NET Core

In your `Program.cs` or `Startup.cs`, configure Swagger UI to include the CSS and JS:

```csharp
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

    // Inject custom CSS
    c.InjectStylesheet("/swagger.bootstrap.min.css");

    // Inject custom JavaScript
    c.InjectJavascript("/swagger.bootstrap.min.js");
});