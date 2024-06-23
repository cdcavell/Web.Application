using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;

namespace ClassLibrary.Mvc.Services.AppSettings
{
    public class AppSettingsService(IOptions<AppSettingsServiceOptions> options) : IAppSettingsService
    {
        private readonly Models.AppSettings _appSettings = (Models.AppSettings)options.Value.AppSettings;
        private readonly string _rawJson = JsonConvert.SerializeObject(options.Value.AppSettings);

        public string AssemblyName()
        {
            return _appSettings.AssemblyName;
        }

        public string AssemblyVersion()
        {
            return _appSettings.AssemblyVersion;
        }

        public string LastModifiedDate()
        {
            return _appSettings.LastModifiedDateTime.ToString("MM/dd/yyyy");
        }

        public string EnvironmentName()
        {
            return _appSettings.EnvironmentName;
        }

        public bool IsDevelopment()
        {
            return _appSettings.IsDevelopment;
        }

        public bool IsProduction()
        {
            return _appSettings.IsProduction;
        }

        public string ToJson()
        {
            return _rawJson;
        }

        public T ToObject<T>()
        {
            T? result = JsonConvert.DeserializeObject<T>(_rawJson);
            return result == null ? throw new NullReferenceException() : result;
        }
    }
}
