using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.Application.Models.Home
{
    public class InputModel
    {
        public const string BindProperties = "ReturnUrl";

        [Required]
        [FromQuery(Name = "ReturnUrl")]
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
