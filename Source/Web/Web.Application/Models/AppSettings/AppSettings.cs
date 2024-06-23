using System.Text.Json.Serialization;

namespace Web.Application.Models.AppSettings
{
    public class AppSettings(IConfiguration configuration) : ClassLibrary.Mvc.Services.AppSettings.Models.AppSettings(configuration)
    {
        public ConnectionStrings ConnectionStrings { get; set; } = new();
    }
}
