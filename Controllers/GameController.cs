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

        [HttpGet("{id}")]
        public IActionResult GetGame(long id)
        {
            var item = _context.Games.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}
