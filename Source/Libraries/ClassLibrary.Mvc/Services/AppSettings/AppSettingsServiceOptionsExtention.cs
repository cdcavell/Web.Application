using Microsoft.Extensions.DependencyInjection;

namespace ClassLibrary.Mvc.Services.AppSettings
{
    public static class AppSettingsServiceOptionsExtention
    {
        public static IServiceCollection AddAppSettingsService(this IServiceCollection serviceCollection, Action<AppSettingsServiceOptions> options)
        {
            serviceCollection.AddScoped<IAppSettingsService, AppSettingsService>();
            if (options == null)
                throw new ArgumentNullException(nameof(options), @"Missing required options for AppSettingsService.");

            serviceCollection.Configure(options);
            return serviceCollection;
        }
    }
}
