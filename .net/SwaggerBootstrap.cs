using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swagger.Bootstrap.Options;
using Swagger.Bootstrap.Utilities;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Swagger.Bootstrap
{
    public static class SwaggerBootstrap
    {
        /// <summary>
        /// Adds Swagger Bootstrap configuration and services to the specified service collection.
        /// </summary>
        /// <remarks>Call this method during application startup to enable Swagger Bootstrap features. The
        /// options configured via the delegate are registered as a singleton and can be accessed throughout the
        /// application's lifetime.</remarks>
        /// <param name="services">The service collection to which the Swagger Bootstrap services will be added.</param>
        /// <param name="configure">An optional delegate to configure the Swagger Bootstrap options. If null, default options are used.</param>
        /// <returns>The same service collection instance so that additional calls can be chained.</returns>
        public static IServiceCollection AddSwaggerBootstrap(
        this IServiceCollection services,
        Action<SwaggerBootstrapOptions>? configure = null)
        {
            var options = new SwaggerBootstrapOptions();
            configure?.Invoke(options);

            services.AddSingleton(options);

            return services;
        }

        /// <summary>
        /// Enables Swagger Bootstrap middleware and configures the Swagger UI for the application.
        /// This method replaces <c>UseSwaggerUI</c> by providing a fully integrated Bootstrap‑based
        /// Swagger UI setup.
        /// </summary>
        /// <remarks>
        /// This method should be called after all other middleware registrations and before the
        /// application starts handling requests. It replaces the standard <c>UseSwaggerUI</c>
        /// configuration and sets up endpoints for Swagger UI and, if enabled in configuration,
        /// authentication and experimental features.
        /// </remarks>
        /// <param name="app">The application builder used to configure the request pipeline.</param>
        /// <param name="setupAction">
        /// An optional action to configure additional Swagger UI options. If null, default options are used.
        /// </param>
        /// <returns>
        /// The original <see cref="IApplicationBuilder"/> instance, enabling method chaining.
        /// </returns>
        public static IApplicationBuilder UseSwaggerBootstrap(this IApplicationBuilder app, Action<SwaggerUIOptions> setupAction = null!)
        {
            var options = app.ApplicationServices.GetRequiredService<SwaggerBootstrapOptions>();
            var routeBuilder = (IEndpointRouteBuilder) app;

            if (options.UseAuthentication)
            {
                MapLoginPage(routeBuilder, options.loginOptions);
            }

            if (options.UseExperimentalFeatures)
            {
                MapExperimentalFeatures(routeBuilder);
            }

            MapSwaggerBootstrap(routeBuilder);

            ConfigureSwaggerUI(app, setupAction);

            return app;
        }

        /// <summary>
        /// Configures and enables the Swagger UI middleware with optional customization for the application's API
        /// documentation interface.
        /// </summary>
        /// <remarks>This method sets up the Swagger UI with default or customized options, including
        /// support for custom stylesheets and JavaScript resources. It also conditionally injects authentication and
        /// experimental feature scripts based on the application's configuration. Call this method during application
        /// startup to expose the Swagger UI for API exploration and testing.</remarks>
        /// <param name="app">The application builder used to configure the middleware pipeline.</param>
        /// <param name="setupAction">An optional delegate to configure additional Swagger UI options. If null, default options are used.</param>
        /// <returns>The application builder with the Swagger UI middleware configured.</returns>
        private static IApplicationBuilder ConfigureSwaggerUI(IApplicationBuilder app, Action<SwaggerUIOptions> setupAction = null!)
        {
            SwaggerUIOptions options;
            using (var scope = app.ApplicationServices.CreateScope())
            {
                options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<SwaggerUIOptions>>().Value;
                setupAction?.Invoke(options);
            }

            // To simplify the common case, use a default that will work with the SwaggerMiddleware defaults
            if (options.ConfigObject.Urls == null)
            {
                var hostingEnv = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
                options.ConfigObject.Urls = [new UrlDescriptor { Name = $"{hostingEnv.ApplicationName} v1", Url = "v1/swagger.json" }];
            }

            // Retrieve the SwaggerBootstrapOptions to determine which resources to inject based on the configuration
            var swaggerBootstrapOptions = app.ApplicationServices.GetRequiredService<SwaggerBootstrapOptions>();

            // Inject the custom CSS and JavaScript resources for Swagger Bootstrap UI
            options.InjectStylesheet("/swaggerbootstrap/main.css");
            options.InjectJavascript("/swaggerbootstrap/main.js");

            // Conditionally inject additional JavaScript resources based on the SwaggerBootstrapOptions configuration
            if (swaggerBootstrapOptions.UseAuthentication)
            {
                options.InjectJavascript("/swaggerbootstrap/authentication.js");
            }

            // Conditionally inject experimental features JavaScript if enabled in the options
            if (swaggerBootstrapOptions.UseExperimentalFeatures)
            {
                options.InjectJavascript("/swaggerbootstrap/experimental.js");
            }

            return app.UseSwaggerUI(options);
        }

        /// <summary>
        /// Configures endpoints to serve the Swagger Bootstrap CSS and JavaScript resources for API documentation UI.
        /// </summary>
        /// <remarks>This method enables clients to access the Swagger UI's custom styling and scripting
        /// by mapping '/swaggerbootstrap/main.css' and '/swaggerbootstrap/main.js' endpoints. These resources are
        /// typically used to enhance or customize the appearance and behavior of the Swagger UI.</remarks>
        /// <param name="app">The endpoint route builder used to map the Swagger Bootstrap resource endpoints.</param>
        private static void MapSwaggerBootstrap(IEndpointRouteBuilder app)
        {
            app.MapGet("/swaggerbootstrap/main.css", () =>
            {
                var content = ResourceLoader.Load($"Resources/main.css");

                return Results.Text(content, "text/css; charset=utf-8");
            });

            app.MapGet("/swaggerbootstrap/main.js", () =>
            {
                var content = ResourceLoader.Load($"Resources/main.js");

                return Results.Text(content, "application/javascript; charset=utf-8");
            });
        }

        /// <summary>
        /// Maps the endpoint that serves the experimental JavaScript resource for Swagger Bootstrap UI.
        /// </summary>
        /// <remarks>This method registers a GET endpoint at '/swaggerbootstrap/experimental.js' that
        /// returns the experimental JavaScript file. Intended for use in development or preview scenarios where
        /// experimental Swagger UI features are being tested.</remarks>
        /// <param name="app">The endpoint route builder used to configure application routing.</param>
        private static void MapExperimentalFeatures(IEndpointRouteBuilder app)
        {
            app.MapGet("/swaggerbootstrap/experimental.js", () =>
            {
                var content = ResourceLoader.Load($"Resources/experimental.js");

                return Results.Text(content, "application/javascript; charset=utf-8");
            });
        }

        /// <summary>
        /// Configures endpoints for serving the login page and authentication script used by SwaggerBootstrap UI.
        /// </summary>
        /// <remarks>This method maps the "/swaggerbootstrap/login.html" and
        /// "/swaggerbootstrap/authentication.js" endpoints to serve the login page and its supporting JavaScript. The
        /// login page is dynamically generated based on the provided options. This method is intended to be called
        /// during application startup as part of endpoint configuration.</remarks>
        /// <param name="app">The endpoint route builder used to map the login page and authentication script endpoints.</param>
        /// <param name="options">The options that control the behavior and appearance of the login page, including endpoint URLs and username
        /// field configuration.</param>
        private static void MapLoginPage(IEndpointRouteBuilder app, LoginPageOptions options)
        {
            app.MapGet("/swaggerbootstrap/login.html", () =>
            {
                var content = ResourceLoader.Load($"Resources/login.html");

                var values = new Dictionary<string, string>
                {
                    ["loginEndpoint"] = options.LoginEndpoint,
                    ["variableName"] = options.VariableName,
                    ["usernameLabel"] = options.UserNameType == UserNameType.Text ? "Username" : "Email",
                    ["usernamePlaceholder"] = options.UserNameType == UserNameType.Text ? "Username" : "Email address",
                    ["usernameType"] = options.UserNameType == UserNameType.Text ? "text" : "email",
                    ["usernameJs"] = options.UserNameType == UserNameType.Text ? "username" : "email"
                };

                content = TemplateEngine.Apply(content, values);

                return Results.Text(content, "text/html; charset=utf-8");
            }); 
            
            app.MapGet("/swaggerbootstrap/authentication.js", () =>
            {
                var content = ResourceLoader.Load($"Resources/authentication.js");

                return Results.Text(content, "application/javascript; charset=utf-8");
            });
        }
    }
}
