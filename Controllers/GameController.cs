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
    public class GameController : Controller
    {
        private readonly BoardGameContext _context;

        public GameController (BoardGameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Game> GetGames()
        {
           return _context.Games.ToList();
        }

        [HttpGet("{id}", Name = "GetGame")]
        public IActionResult GetGame(long id)
        {
            var item = _context.Games.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Game item)
        {
            if (item == null)
            {
                return BadRequest();
            }


            var game = new Game {Title = item.Title};
            _context.Games.Add(game);
            _context.SaveChanges();

            return CreatedAtRoute("GetGame", new { id = game.Id }, game);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Game item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var game = _context.Games.FirstOrDefault(t => t.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            game.Title = item.Title;

            _context.Games.Update(game);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var game = _context.Games.First(t => t.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
