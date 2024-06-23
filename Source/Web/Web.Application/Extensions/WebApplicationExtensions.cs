using ClassLibrary.Data;
using ClassLibrary.Data.Contexts;
using ClassLibrary.Mvc.Exceptions.Handlers;
using ClassLibrary.Mvc.Services.AppSettings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using Web.Application.Models.AppSettings;
using Web.Application.Services;

namespace Web.Application.Extensions
{
    internal static class WebApplicationExtensions
    {
        private static AppSettings? _appSettings;

        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((ctx, lc) => lc
                .ReadFrom.Configuration(ctx.Configuration));

            _appSettings = new(builder.Configuration);
            builder.Services.AddAppSettingsService(options =>
            {
                options.AppSettings = _appSettings;
            });

            builder.Services.AddDataProtection().UseCryptographicAlgorithms(
                new AuthenticatedEncryptorConfiguration
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                });

            string migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name
                ?? throw new NullReferenceException("Unable to acquire assembly name");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    _appSettings.ConnectionStrings.ApplicationDbConnection,
                    sql => {
                        sql.MigrationsAssembly(migrationsAssembly);
                        sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    }
                ));

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

            // Backup Database
            ApplicationDbBackup.Run(app);

            // Perform Migrations
            foreach (string message in ApplicationDbContext.Migrate(app))
                Log.Information(message);

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
