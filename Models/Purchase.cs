using System.ComponentModel.DataAnnotations;

namespace BoardGameTracker.Models {
    public class Purchase : Entity {
        public int Id {get; set;}
        public int GameId {get; set;}
        [Required]
        public int AccountId {get; set;}

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double Amount {get; set;}

        public Game Game {get; set;}
        public Account Account {get; set;}
    }
}