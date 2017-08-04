using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoardGameTracker.Models {
    public class Account : Entity {
        public int Id {get; set;}
        
        [EmailAddress]
        [Required]
        public string Email {get; set;}

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public List<Purchase> Purchases {get; set;}
    }
}