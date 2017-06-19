using System.Collections.Generic;

namespace BoardGameTracker.Models {
    public class User {
        public int Id {get; set;}
        public string Email {get; set;}

        public List<Purchase> Purchases {get; set;}
    }
}