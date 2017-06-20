using Microsoft.EntityFrameworkCore;
using BoardGameTracker.Models;

namespace BoardGameTracker.Data {
    public class BoardGameContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<User> Users {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=database.db");
        }
    }

}