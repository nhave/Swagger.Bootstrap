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
- Authentication window, to authorize Swagger using JWT.
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
## **Version 2.X *(NEW)***
Version 2.X has returned to `UseSwaggerBootstrap` instead of `UseSwaggerUI`

In your `Program.cs` or `Startup.cs`, add the following methods.
```csharp
builder.Services.AddSwaggerBootstrap();
```
Or
```csharp
// All variables are optional.
builder.Services.AddSwaggerBootstrap(options =>
{
    // Enables the experimental features,
    // including navigation & tools.
    options.UseExperimentalFeatures = true;
    // Enables the login page and button.
    options.UseAuthentication = true;
    // The endpoint to call during login.
    options.loginOptions.LoginEndpoint = "/auth/login";
    // The variable to use for authentication, when login succeeds.
    options.loginOptions.VariableName = "jwtToken";
    // Whether to configure the username field as text or email.
    // The label will be changed as well.
    options.loginOptions.UserNameType = UserNameType.Text;
});
```
Then, replace the default `UseSwaggerUI` with:
```csharp
app.UseSwaggerBootstrap(c =>
{
    // OpenApi example, using swagger gen.
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
```
`UseStaticFiles` is no longer needed as the resources are dynamically mapped now.

## **Version 1.x**
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

## **PRE 1.0**

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