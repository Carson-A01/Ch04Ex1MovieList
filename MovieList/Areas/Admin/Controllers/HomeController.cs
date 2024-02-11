using Microsoft.AspNetCore.Mvc;
using MovieList.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace MovieList.Controllers
{
    [Area("Admin")]
    public class AdminHomeController : Controller
    {
        private MovieContext context { get; set; }
        public AdminHomeController(MovieContext cxt) => context = cxt;

        public IActionResult Index()
        {
            var movies = context.Movies.Include(m => m.Genre).OrderBy(m => m.Name).ToList();
            return View(movies);
        }
        public IActionResult StaticContent()
        {
            return Content("Static Page from Custom URL");
        }
        [Route("{Controller=Home}/{Action=Attribute}")]
        public IActionResult Attribute()
        {
            return Content("Static Attribute Page.");
        }
    }
}
