using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KOPERASI_TANTERA.Web.Models.Entities
{
    public class Member : IdentityUser
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(15)]
        public string ICNumber { get; set; }
        
        [Required]
        public bool IsAcceptTC { get; set; }

    }
}
