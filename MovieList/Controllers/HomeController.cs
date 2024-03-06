using Microsoft.AspNetCore.Mvc;
using MovieList.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace MovieList.Controllers
{
    public class HomeController : Controller
    {
        private MovieContext context { get; set; }
        public HomeController(MovieContext cxt) => context = cxt;

        public IActionResult Index(string activeConf = "all", string activeDiv = "all")
        {

            var session = new MovieSession(HttpContext.Session);
            session.SetActiveConf(activeConf);
            session.SetActiveDiv(activeDiv);
            int? count = session.GetMyMovieCount();
            if (count == null)
            {
                var cookies = new MovieCookies(Request.Cookies);
                string[] ids = cookies.GetMyMovieIds();

                List<Movie> myMovies = new List<Movie>();
                if (ids.Length > 0)
                {
                    myMovies = context.Movies.Include(t => t.Name).Include(t => t.Year).Include(t => t.Rating).Include(t => t.GenreId).Where(t => ids.Contains(t.MovieId.ToString())).ToList();
                }
                session.SetMyMovies(myMovies);
            }
            var movies = context.Movies.Include(m => m.Genre).OrderBy(m => m.Name).ToList();
            return View(movies);
        }
        public ViewResult Details(string id)
        {
            var session = new MovieSession(HttpContext.Session);
            var model = new List<Movie>
            {
                MovieContext = context.Movies.Include(m => m.Name).Include(m => m.Year).Include(m => m.Rating).Include(m => m.GenreId).FirstOrDefault(m => m.MovieId.ToString() == id),
                activeConf = session.GetActiveConf(),
                activeDiv = session.GetActiveDiv()
            };
            return View(model);
        }
        [HttpPost]
        public RedirectToActionResult Add(Movie model)
        {
            var session = new MovieSession(HttpContext.Session);
            var movies = session.GetMyMovies;
            movies.Add(model);
            session.SetMyMovies(movies);
            TempData["message"] = $"(model.Name) added to favorites";
            return RedirectToAction("Index", new { activeConf = session.GetActiveConf(), activeDiv = session.GetActiveDiv() });
        }
        public IActionResult Static()
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
