using System.Data;

namespace ClassLibrary.Mvc.Services.AppSettings
{
    public interface IAppSettingsService
    {
        public string AssemblyName();

        public string AssemblyVersion();

        public string LastModifiedDate();

        public string EnvironmentName();

        public bool IsDevelopment();

        public bool IsProduction();

        public string ToJson();

        public T ToObject<T>();
    }
}
