using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoardGameTracker.Models {
    public class User : Entity {
        public int Id {get; set;}
        
        [EmailAddress]
        [Required]
        public string Email {get; set;}

        public List<Purchase> Purchases {get; set;}
    }
}