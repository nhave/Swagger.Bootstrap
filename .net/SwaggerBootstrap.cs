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
            Action<SwaggerUIOptions> setupAction = null!)
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

            options.AddSwaggerBootstrap();

            app.UseStaticFiles();

            return app.UseSwaggerUI(options);
        }

        public static SwaggerBootstrapOptions AddSwaggerBootstrap(this SwaggerUIOptions swagger)
        {
            swagger.InjectStylesheet("/_content/Swagger.Bootstrap/swagger.bootstrap.css");
            swagger.InjectJavascript("/_content/Swagger.Bootstrap/swagger.bootstrap.js");

            return new SwaggerBootstrapOptions(swagger);
        }

    }

    public class SwaggerBootstrapOptions
    {
        private SwaggerUIOptions swagger;
        public SwaggerBootstrapOptions(SwaggerUIOptions swagger)
        {
            this.swagger = swagger;
        }

        public SwaggerBootstrapOptions AddExperimentalFeatures()
        {
            swagger.InjectJavascript("/_content/Swagger.Bootstrap/swagger.bootstrap.experimental.js");
            return this;
        }
    }
}
