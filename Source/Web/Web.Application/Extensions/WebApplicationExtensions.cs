using ClassLibrary.Mvc.Exceptions.Handlers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using Web.Application.Services;

namespace Web.Application.Extensions
{
    internal static class WebApplicationExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((ctx, lc) => lc
                .ReadFrom.Configuration(ctx.Configuration));

            builder.Services.AddDataProtection().UseCryptographicAlgorithms(
                new AuthenticatedEncryptorConfiguration
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMvc();
            builder.Services.AddControllersWithViews();
            builder.Services.AddResponseCaching();

            // Exception Handlers are called in the order they are registered
            builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
            builder.Services.AddExceptionHandler<ArgumentExceptionHandler>();
            builder.Services.AddExceptionHandler<BadHttpRequestExceptionHandler>();
            builder.Services.AddExceptionHandler<UnauthorizedAccessExceptionHandler>();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            // Hosted Service
            builder.Services.AddSingleton<TimedHostedService>();
            builder.Services.AddHostedService(p => p.GetRequiredService<TimedHostedService>());

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseResponseCaching();
            app.UseExceptionHandler("/Error/500");
            app.UseStatusCodePages(subApp =>
            {
                subApp.Run(async context =>
                {
                    int statusCode = context.Response.StatusCode;
                    context.Response.Redirect($"/Status/{statusCode}");
                    await context.Response.StartAsync().ConfigureAwait(false);
                });
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            return app;
        }
    }
}
