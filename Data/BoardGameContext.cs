using Microsoft.EntityFrameworkCore;
using BoardGameTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BoardGameTracker.Data {
    public class BoardGameContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Account> Accounts {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=database.db");
        }
    }

}