using Microsoft.AspNetCore.Mvc;

namespace MovieList.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            int num = HttpContext.Session.GetInt32("num") ?? 0;
            num += 1;
            HttpContext.Session.SetInt32("num", num);
            var options = new CookieOptions { Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append("username", "training", options);
            return View();
        }

        public IActionResult Cookie()
        {
            var username = Request.Cookies["username"];
            ViewBag.Username = username;
            return View();
        }
    }
}
