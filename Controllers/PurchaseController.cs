using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGameTracker.Data;
using BoardGameTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameTracker.Controllers
{
    [Route("api/[controller]")]
    public class PurchaseController : Controller
    {
        private readonly BoardGameContext _context;

        public PurchaseController (BoardGameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Purchase> GetPurchases()
        {
           return _context.Purchases.ToList();
        }

        [HttpGet("{id}", Name = "GetPurchase")]
        public IActionResult GetPurchase(long id)
        {
            var item = _context.Purchases.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Purchase item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            // Check AccountId exists
            if (_context.Accounts.FirstOrDefault(u => u.Id == item.AccountId) == null) {
                return BadRequest();
            }
            
            // Check if the posted Purchase has a valid game, if not it creates it
            Game game = null;
            if (item.Game != null && _context.Games.FirstOrDefault(g => g.Id == item.GameId) == null || _context.Games.FirstOrDefault(g => g.Id == item.Game.Id) == null) 
            {
                game = new Game();
                game.Title = item.Game.Title;
                _context.Games.Add(game);
                _context.SaveChanges();
            }

            if (game == null) {
                game = item.Game ?? _context.Games.FirstOrDefault(g => g.Id == item.GameId);
            }

            var purchase = new Purchase();
            purchase.Amount = item.Amount;
            purchase.GameId = game.Id;
            purchase.AccountId = item.AccountId;

            _context.Purchases.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetPurchase", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [Bind("GameId", "AccountId", "Amount")] Purchase item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var purchase = _context.Purchases.FirstOrDefault(t => t.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            purchase.GameId = item.GameId;
            purchase.AccountId = item.AccountId;
            purchase.Amount = item.Amount;

            _context.Purchases.Update(purchase);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var purchase = _context.Purchases.First(t => t.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            _context.Purchases.Remove(purchase);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
