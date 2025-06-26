using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InstrumentKaHealth.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string Department { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}