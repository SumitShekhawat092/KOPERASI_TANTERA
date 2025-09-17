using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KOPERASI_TANTERA.Web.Models.Auth
{
    public class StepOneModel
    {
        [Required]
        [StringLength(200)]
        public string CustomerName { get; set; } = string.Empty;
        [Required]
        [StringLength(15)]
        public string ICNumber { get; set; } = string.Empty;
        [Required]
        [StringLength(15)]
        public string MobileNumber { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [StringLength(250)]
        public string EmailAddress { get; set; } = string.Empty;
        public string PasswordPIN { get; set; } = string.Empty;
    }
}
