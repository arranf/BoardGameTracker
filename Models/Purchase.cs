namespace BoardGameTracker.Models {
    public class Purchase {
        public int Id {get; set;}
        public int GameId {get; set;}
        public int UserId {get; set;}
        public double Amount {get; set;}

        public Game Game {get; set;}
        public User User {get; set;}
    }
}