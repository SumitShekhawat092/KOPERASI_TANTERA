using System.ComponentModel.DataAnnotations;

namespace KOPERASI_TANTERA.Web.Models.Entities
{
    public class Member
    {
        public Guid MemberId { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(15)]
        public string ICNumber { get; set; }
        [Required]
        [StringLength(15)]
        public string Mobile{ get; set; }
        [Required]
        [StringLength(250)]
        public string Email { get; set; }
        [Required]
        public bool IsAcceptTermsAndConditions { get; set; }

    }
}
