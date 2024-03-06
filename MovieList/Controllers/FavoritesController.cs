using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieList.Models;

namespace MovieList.Controllers
{
    public class FavoritesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var movie = HttpContext.Session.GetObject<Movie>("movie") ?? new Movie();
            movie.Name = "Saving Private Ryan";
            HttpContext.Session.SetObject("movie", movie);

            var session = new MySession(HttpContext.Session);
            var movies = session.getMovies;
            movies.Add(new Movie { "Stand By Me", 1986, 5, "T" });
            session.SetMovies(movies);

            return View(movie);
        }

        [HttpPost]
        public RedirectToActionResult Delete()
        {
            var session = new MovieSession(HttpContext.Session);
            var cookies = new MovieCookies(Response.Cookies);
            session.RemoveMyMovies();
            cookies.RemoveMyMovieIds();

            TempData["message"] = "Favorite movies cleared";
            return RedirectToAction("Index", "Home", new { activeConf = session.GetActiveConf(), activeDiv = session.GetActiveDiv() });
        }

    }
}
