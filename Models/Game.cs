using System.ComponentModel.DataAnnotations;

namespace BoardGameTracker.Models {
    public class Game : Entity {
        public int Id {get; set;}

        [Required]
        public string Title {get; set; }
    }
}