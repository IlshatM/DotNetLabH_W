using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

namespace MVCMovie.Controllers
{
    public class GameController : Controller
    {
        private readonly MvcMovieContext _context;

        public GameController(MvcMovieContext context)
        {
            _context = context;
        }
        // GET
        public async Task<IActionResult> Index()
        {
            var games = _context.Game
                .Select(g => g)
                .Include(g=>g.BasedOnGameMovie);
            return View(await games.ToListAsync());
        }
    }
}