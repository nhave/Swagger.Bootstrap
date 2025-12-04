<div align="center">

[![Latest Release](https://img.shields.io/github/v/release/nhave/Swagger.Bootstrap?style=for-the-badge)](https://github.com/nhave/Swagger.Bootstrap/releases)
[![License: NonCommercial](https://img.shields.io/badge/License-NonCommercial-orange?style=for-the-badge)](https://github.com/nhave/Swagger.Bootstrap/blob/main/LICENSE)
[![Open Issues](https://img.shields.io/github/issues/nhave/Swagger.Bootstrap?style=for-the-badge)](https://github.com/nhave/Swagger.Bootstrap/issues)
[![Stars](https://img.shields.io/github/stars/nhave/Swagger.Bootstrap?style=for-the-badge)](https://github.com/nhave/Swagger.Bootstrap/stargazers)

</div>

# Swagger.Bootstrap

**Swagger.Bootstrap** is an add-on for [Swagger UI](https://swagger.io/tools/swagger-ui/) that gives it a modern [Bootstrap](https://getbootstrap.com/) look and feel.  
It also adds a simple theme switcher so users can toggle between **Light**, **Dark**, **Oled** and **System**.

## ✨ Features
- Bootstrap-inspired styling for Swagger UI
- Built-in theme switcher (light/dark)
- Navigation & Tool menus (Experimental)
- Easy integration with ASP.NET Core Swagger setup

## 📂 Getting Started

## 1. NuGet package manager
Install using the NuGet package manager or by running one of the following commands.
```bash
dotnet add package Swagger.Bootstrap
```
```bash
Install-Package Swagger.Bootstrap
```
## New setup
In your `UseSwaggerUI` just add `AddSwaggerBootstrap`:
```csharp
app.UseSwaggerUI(options =>
{
    options.AddSwaggerBootstrap();
});
```
To enable the experimental features, add `AddExperimentalFeatures` to `AddSwaggerBootstrap`:
```csharp
options.AddSwaggerBootstrap()
        .AddExperimentalFeatures();
```
This setup needs `UseStaticFiles` to be enabled:
```csharp
app.UseStaticFiles();
```

## Old setup

### Configure Swagger.Bootstrap
In your `Program.cs` or `Startup.cs`, simply replace the default UseSwaggerUI with:

```csharp
app.UseSwaggerBootstrap(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
```

Or if you want to keep your existing SwaggerUI configuration:  
(app.UseStaticFiles must be set)

```csharp
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.AddSwaggerBootstrap();
});

app.UseStaticFiles();
```

## 2. Manual installation
### Download the files
Copy the provided `swagger.bootstrap.min.css` and `swagger.bootstrap.min.js` files into your projects static file location, wwwroot.

### Enable static files
Add the following, to your `Program.cs` or `Startup.cs`
```csharp
app.UseStaticFiles();
```

### Inject into Swagger UI
```csharp
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

    // Inject custom CSS
    c.InjectStylesheet("/swagger.bootstrap.min.css");

    // Inject custom JavaScript
    c.InjectJavascript("/swagger.bootstrap.min.js");
});
```