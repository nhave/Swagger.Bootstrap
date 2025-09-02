using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerBootstrap
    {
        public static IApplicationBuilder UseSwaggerBootstrap(
            this IApplicationBuilder app,
            Action<SwaggerUIOptions> setupAction = null)
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

            options.InjectStylesheet("/_content/Swagger.Bootstrap/swagger.bootstrap.min.css");
            options.InjectJavascript("/_content/Swagger.Bootstrap/swagger.bootstrap.min.js");

            app.UseStaticFiles();

            return app.UseSwaggerUI(options);
        }
    }
}
